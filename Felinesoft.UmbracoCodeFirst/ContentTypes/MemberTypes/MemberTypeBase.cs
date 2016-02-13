using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Felinesoft.UmbracoCodeFirst.Extensions;

using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.Converters;
using Felinesoft.UmbracoCodeFirst.Exceptions;
using Felinesoft.UmbracoCodeFirst.Core.Modules;

namespace Felinesoft.UmbracoCodeFirst.ContentTypes
{
    /// <summary>
    /// A base class for code-first member types
    /// </summary>
    [DoNotRemoveProperty("umbracoMemberComments")]
    [DoNotRemoveProperty("umbracoMemberFailedPasswordAttempts")]
    [DoNotRemoveProperty("umbracoMemberApproved")]
    [DoNotRemoveProperty("umbracoMemberLockedOut")]
    [DoNotRemoveProperty("umbracoMemberLastLockoutDate")]
    [DoNotRemoveProperty("umbracoMemberLastLogin")]
    [DoNotRemoveProperty("umbracoMemberLastPasswordChangeDate")]
    [DoNotRemoveProperty("umbracoMemberPasswordRetrievalQuestion")]
    [DoNotRemoveProperty("umbracoMemberPasswordRetrievalAnswer")]
    [DoNotRemoveTab("Membership")]
    public abstract class MemberTypeBase : CodeFirstContentBase<MemberNodeDetails>
    {
        private Lazy<IMemberModelModule> _modelModule;

        /// <summary>
        /// A base class for code-first document types.
        /// This constructor initialises the NodeDetails property with an empty instance of <see cref="DocumentNodeDetails"/>
        /// </summary>
        public MemberTypeBase()
        {
            NodeDetails = new MemberNodeDetails();

            _modelModule = new Lazy<IMemberModelModule>(() => CodeFirstManager.Current.Modules.MemberModelModule);
        }

        #region Member Properties
        public string Email { get; set; }

        public bool IsApproved { get; set; }

        public bool IsLockedOut { get; set; }

        public DateTime LastLockoutDate { get; set; }

        public DateTime LastLoginDate { get; set; }

        public DateTime LastPasswordChangeDate { get; set; }

        public string Name
        {
            get
            {
                return NodeDetails.Name;
            }
            set
            {
                NodeDetails.Name = value;
            }
        }

        public string Username { get; set; }

        public string Comments { get; set; }

        public string PasswordQuestion { get; set; }

        public string PasswordAnswer { get; set; }

        public int FailedPasswordAttempts { get; set; }
        #endregion

        #region Create and update IContent
        /// <summary>
        /// Persists the current values of the instance back to the database
        /// </summary>
        /// <param name="contentId">Id of the Umbraco Document</param>
        /// <param name="parentId">Id of the parent Umbraco Document. Only applied when creating new content. At present code-first cannot change the parent of an existing node.</param>
        /// <param name="userId">The user ID for the audit trail</param>
        /// <param name="raiseEvents">True to raise Umbraco content service events</param>
        public void Persist(bool raiseEvents = false)
        {
            IMember content;
            if (_modelModule.Value.TryConvertToContent(this, out content))
            {
                //persist object into umbraco database
                ApplicationContext.Current.Services.MemberService.Save(content, raiseEvents);

                //update the node details
                NodeDetails = new MemberNodeDetails(content);
            }
            else
            {
                throw new CodeFirstException("Failed to convert model to content.");
            }
        }

        public void Project(IMember target)
        {
            _modelModule.Value.ProjectModelToContent(this, target);
        }
        #endregion
    }
}