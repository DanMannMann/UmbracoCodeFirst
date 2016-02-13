using Felinesoft.UmbracoCodeFirst.TestTarget.TestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ts = Felinesoft.UmbracoCodeFirst.TestTarget.TypeSet3;
using Felinesoft.UmbracoCodeFirst.Linq;
using Umbraco.Web;

namespace Felinesoft.UmbracoCodeFirst.TestTarget.Tests
{
    public class ContentTests : TestBase, ICodeFirstTest
    {
        public void Run()
        {
            Initialise("TypeSet3");
            ts.MasterRenamed mas = new ts.MasterRenamed()
            {
                MasterTab = new ts.MasterTab()
                {
                    MasterDatePickerTab = new DataTypes.BuiltIn.DatePicker() { Value = DateTime.Parse("2015-09-21 16:54:23.000") },
                    MasterRichtextEditorTab = new DataTypes.BuiltIn.RichtextEditor() { Value = "<p>this is <strong>STRONG</strong></p>" },
                    MasterTextstringTab = new DataTypes.BuiltIn.Textstring() { Value = "TEXTSTRINGS" }
                },
                MasterTextstringRenamedRoot = new DataTypes.BuiltIn.Textstring() { Value = "OTHERONES!!" },
                MasterTrueFalseRoot = new DataTypes.BuiltIn.TrueFalse() { Value = true }
            };
            mas.NodeDetails.Name = "Master Test";
            mas.Persist(publish: true);

            for (int i = 0; i < 20; i++)
            {
                ts.Child2 ch2 = GetChild();
                ch2.NodeDetails.Name = Guid.NewGuid().ToString();
                ch2.Persist(mas.NodeDetails.UmbracoId, publish: true);
            }

            umbraco.library.RefreshContent();
            var pubmas = new UmbracoHelper(UmbracoContext.Current).TypedContent(mas.NodeDetails.UmbracoId);
            var kids = pubmas.ChildrenOfType<ts.Child2>().ToList();
            var trd = kids.First().Composition.CompositionNumericRoot;
        }

        private ts.Child2 GetChild()
        {
            return new ts.Child2()
            {
                Child2NumericRoot = new DataTypes.BuiltIn.Numeric() { Value = 69 },
                Child2TextstringRoot = new DataTypes.BuiltIn.Textstring() { Value = "STRIIIING" },
                Child2TrueFalseRoot = new DataTypes.BuiltIn.TrueFalse() { Value = true },
                MasterTextstringRenamedRoot = new DataTypes.BuiltIn.Textstring() { Value = "FAAAACE" },
                MasterTrueFalseRoot = new DataTypes.BuiltIn.TrueFalse() { Value = false },
                MasterTab = new ts.MasterTab()
                {
                    MasterDatePickerTab = new DataTypes.BuiltIn.DatePicker() { Value = DateTime.Now },
                    MasterRichtextEditorTab = new DataTypes.BuiltIn.RichtextEditor() { Value = "AAARRRSSSEE" },
                    MasterTextstringTab = new DataTypes.BuiltIn.Textstring() { Value = "INNIT" }
                },
                Child2Tab = new ts.Child2Tab()
                {
                    Child2DatePickerTab = new DataTypes.BuiltIn.DatePicker() { Value = DateTime.Now.AddDays(2) },
                    Child2RichtextEditorTab = new DataTypes.BuiltIn.RichtextEditor() { Value = "AAARRRSSSEE 2" },
                    CommonTabBaseTrueFalse = new DataTypes.BuiltIn.TrueFalse() { Value = true },
                    FolderPicker = new DataTypes.BuiltIn.SingleMediaPicker<ContentTypes.MediaFolder>()
                },
                Composition = new ts.Child2Composition()
                {
                    CompositionNumericRoot = new DataTypes.BuiltIn.Numeric() { Value = 96 },
                    CompositionTab = new ts.CompositionTab()
                    {
                        CompositionTabPicker = new DataTypes.BuiltIn.MediaPicker<ts.MediaChild1>(),
                        CompositionMultitext = GetMultiString("hello", "world", "wello", "horld")
                    }
                }
            };
        }

        private DataTypes.BuiltIn.MultipleTextstring GetMultiString(params string[] input)
        {
            var res = new DataTypes.BuiltIn.MultipleTextstring();
            input.ToList().ForEach(x => res.Add(x));
            return res;
        }
    }
}