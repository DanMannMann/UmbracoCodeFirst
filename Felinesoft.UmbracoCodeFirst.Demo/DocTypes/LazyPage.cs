using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;

namespace Felinesoft.UmbracoCodeFirst.Demo.DocTypes
{
    [DocumentType(allowAtRoot: true)]
    [Template(isDefault: true)]
    public class LazyPage : DocumentTypeBase
    {
        private Textstring _ts;
        private TextboxMultiple _tm;
        private RelatedLinks _rl;
        private TestComp1 _tp;

        [ContentProperty]
        public virtual Textstring Text
        {
            get { return _ts; }
            set { _ts = value; }
        }

        [ContentProperty]
        public virtual TextboxMultiple TextMulti
        {
            get { return _tm; }
            set { _tm = value; }
        }

        [ContentProperty]
        public virtual RelatedLinks Links
        {
            get { return _rl; }
            set { _rl = value; }
        }
        
        [ContentComposition]
        public virtual TestComp1 Compo
        {
            get { return _tp; }
            set { _tp = value; }
        }

        public int LazyCount
        {
            get
            {
                int i = 0;
                if (_ts == null) i++;
                if (_tm == null) i++;
                if (_rl == null) i++;
                if (_tp == null) i++;
                return i;
            }
        }
    }
}