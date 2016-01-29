using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;
using Felinesoft.UmbracoCodeFirst.Linq;

namespace Felinesoft.UmbracoCodeFirst.Debug.DocTypes
{
	//The ListViewDocumentType automatically adds the specified generic doc type to AllowedChildren, in this case typeof(Question). 
	//It will be the *only* allowed type, so that the IEnumerable<T> interface is properly honoured.
	[DocumentType(enableListView: true, allowAtRoot: false)]
	[EventHandler(typeof(QuestionSetSurfaceController))]
	[Template(true)]
	public class QuestionSet : ListViewDocumentType<Question>
	{
		[ContentProperty]
		public virtual RichtextEditor WelcomeParagraph { get; set; }

		[ContentProperty(mandatory: false)]
		public virtual QuestionImage DefaultImageForSet { get; set; }
	}
}