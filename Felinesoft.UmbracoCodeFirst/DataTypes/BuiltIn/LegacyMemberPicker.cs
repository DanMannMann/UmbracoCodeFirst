
using Marsman.UmbracoCodeFirst;
using Marsman.UmbracoCodeFirst.ContentTypes;
using Marsman.UmbracoCodeFirst.DataTypes;
using Marsman.UmbracoCodeFirst.DataTypes.BuiltIn;
using Marsman.UmbracoCodeFirst.Attributes;
using Marsman.UmbracoCodeFirst.Extensions;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Umbraco.Core.Models;
using System;
using Marsman.UmbracoCodeFirst.Core;
using Marsman.UmbracoCodeFirst.Exceptions;

namespace Marsman.UmbracoCodeFirst.DataTypes.BuiltIn
{
    [DataType("Umbraco.MemberPicker", "Member Picker")]
    [DoNotSyncDataType][BuiltInDataType]
    public class LegacyMemberPicker : MemberItem, IUmbracoIntegerDataType
    {
		public static implicit operator LegacyMemberPicker(int value)
		{
			return new LegacyMemberPicker() { MemberId = value };
		}

		public static implicit operator LegacyMemberPicker(MemberTypeBase value)
		{
			try
			{
				return new LegacyMemberPicker() { MemberId = value.NodeDetails.UmbracoId };
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Member Id (MemberTypeBase.NodeDetails.UmbracoId) must be set", ex);
			}
		}

		/// <summary>
		/// Initialises the instance from the db value
		/// </summary>
		public void Initialise(int dbValue)
        {
            MemberId = dbValue;
        }

        /// <summary>
        /// Serialises the instance to the db value
        /// </summary>
        public int Serialise()
        {
            return MemberId;
        }
    }
}