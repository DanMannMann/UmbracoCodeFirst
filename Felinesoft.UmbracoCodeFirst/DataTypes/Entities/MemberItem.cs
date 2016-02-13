using Felinesoft.UmbracoCodeFirst.Exceptions;
using System;
using Felinesoft.UmbracoCodeFirst.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Felinesoft.UmbracoCodeFirst.DataTypes
{
    public class MemberItem : XPathItem
    {
        private string _comments;
        private bool _approved;
        private bool _lockedOut;
        private DateTime _lastLogin;
        private DateTime _lastPasswordChange;
        private string _name;
        private string _email;
        private string _loginName;
        private bool _initialised;
        private bool _idSet;
        private int _memberId;

        public int MemberId
        {
            get { return _memberId; }
            set { _idSet = true; _memberId = value; }
        }

        public bool Approved
        {
            get { init(); return _approved; }
            protected set { _approved = value; }
        }

        public bool LockedOut
        {
            get { init(); return _lockedOut; }
            protected set { _lockedOut = value; }
        }

        public DateTime LastLogin
        {
            get { init(); return _lastLogin; }
            protected set { _lastLogin = value; }
        }

        public DateTime LastPasswordChange
        {
            get { init(); return _lastPasswordChange; }
            protected set { _lastPasswordChange = value; }
        }

        public string Name
        {
            get { init(); return _name; }
            protected set { _name = value; }
        }

        public string Email
        {
            get { init(); return _email; }
            protected set { _email = value; }
        }

        public string LoginName
        {
            get { init(); return _loginName; }
            protected set { _loginName = value; }
        }

        public string Comments
        {
            get { init(); return _comments; }
            protected set { _comments = value; }
        }

        public bool HasErrors { get; private set; }

        protected virtual void init()
        {
            if (!_initialised)
            {
                if (_idSet)
                {
                    var member = umbraco.library.GetMember(MemberId);

                    try { Comments = GetCData(member, "umbracoMemberComments"); }
                    catch { HasErrors = true; }
                    try { LastPasswordChange = DateTime.Parse(GetCData(member, "umbracoMemberLastPasswordChangeDate")); }
                    catch { HasErrors = true; }
                    try { LastLogin = DateTime.Parse(GetCData(member, "umbracoMemberLastLogin")); }
                    catch { HasErrors = true; }
                    try { LockedOut = GetCData(member, "umbracoMemberLockedOut") == "1" ? true : false; }
                    catch { HasErrors = true; }
                    try { Approved = GetCData(member, "umbracoMemberApproved") == "1" ? true : false; }
                    catch { HasErrors = true; }
                    try { Email = GetAttribute(member, "email"); }
                    catch { HasErrors = true; }
                    try { Name = GetAttribute(member, "nodeName"); }
                    catch { HasErrors = true; }
                    try { LoginName = GetAttribute(member, "loginName"); }
                    catch { HasErrors = true; }

                    _initialised = true;
                }
                else
                {
                    throw new CodeFirstException("No ID is set");
                }
            }
        }

        public override string ToString()
        {
            if (_idSet)
            {
                return Name;
            }
            else
            {
                return "";
            }
        }
    }
}
