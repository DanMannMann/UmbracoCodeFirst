using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marsman.UmbracoCodeFirst.Extensions;
using Umbraco.Core.Services;
using Umbraco.Core.Models;
using Marsman.UmbracoCodeFirst.Attributes;
using Marsman.UmbracoCodeFirst.Core.Modules;
using Marsman.Reflekt;
using Marsman.UmbracoCodeFirst.Core;

namespace Marsman.UmbracoCodeFirst.ContentTypes
{
    public abstract class MemberGroupBase
    {
        private IMemberGroupService GroupService { get { return Umbraco.Core.ApplicationContext.Current.Services.MemberGroupService; } }
        private IMemberService MemberService { get { return Umbraco.Core.ApplicationContext.Current.Services.MemberService; } }

        public string Name { get; protected set; }

        public MemberGroupBase()
        {
            var attr = this.GetType().GetCodeFirstAttribute<MemberGroupAttribute>();
            Name = attr == null ? this.GetType().Name.ToProperCase() : attr.Name;
        }

        public virtual void AddToGroup(MemberTypeBase member, bool raiseEvents = false)
        {
            MemberService.AssignRole(member.NodeDetails.UmbracoId, Name);
        }

        public virtual void RemoveFromGroup(MemberTypeBase member, bool raiseEvents = false)
        {
            MemberService.DissociateRole(member.NodeDetails.UmbracoId, Name);
        }

        public virtual bool IsInGroup(MemberTypeBase member)
        {
            return MemberService.GetAllRoles(member.NodeDetails.UmbracoId).Contains(Name);
        }

        public virtual IEnumerable<T> GetMembers<T>() where T : MemberTypeBase
        {
            MemberTypeRegistration reg;
            if (CodeFirstManager.Current.Modules.MemberTypeModule.TryGetMemberType(typeof(T), out reg))
            {
                return MemberService.GetMembersInRole(Name).Where(x => x.ContentTypeAlias == reg.Alias).Select(x => x.ConvertToModel<T>());
            }
            else
            {
                return new List<T>();
            }
        }

        public virtual IEnumerable<MemberTypeBase> GetAllMembers()
        {
            var list = MemberService.GetMembersInRole(Name);
            var result = new List<MemberTypeBase>();

            foreach (var memberType in list.Select(x => x.ContentTypeAlias).Distinct().ToList())
            {
                MemberTypeRegistration reg;
                if (CodeFirstManager.Current.Modules.MemberTypeModule.TryGetMemberType(memberType, out reg))
                {
                    var method = Reflekt<IMember>.Method<MemberTypeBase>().WithTypeArguments(reg.ClrType).Parameters<CodeFirstModelContext>(x => x.ConvertToModel<MemberTypeBase>);
                    result.AddRange(list.Where(x => x.ContentTypeAlias == memberType).Select(x => (MemberTypeBase)method.Invoke(null, new object[] { x, null })));
                }
            }

            return result;
        }

        public virtual IEnumerable<IMember> GetRawMembers()
        {
            return MemberService.GetMembersInRole(Name);
        }
    }
}
