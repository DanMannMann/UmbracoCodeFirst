
namespace Felinesoft.UmbracoCodeFirst
{
    public sealed class Features
    {
        internal Features()
        {
            //Set internal defaults here
            WriteLogOutput = false;
            HideCodeFirstEntitiesInTrees = false;
            UseBuiltInPrimitiveDataTypes = true;
            AllowAllMediaTypesInDefaultFolder = true;
            UseBuiltInMediaTypes = true;
            UseTraversalAttributes = false;
            EnablePerformanceDiagnosticTimer = false;
        }

        /// <summary>
        /// <para>
        /// Write output to the standard log file
        /// </para>
        /// <para>
        /// Status: Stable (default: false)
        /// </para>
        /// </summary>
        public bool WriteLogOutput { get; set; }

        /// <summary>
        /// <para>
        /// Hide any code-first-managed content or data type from the trees in the Umbraco back-office
        /// </para>
        /// <para>
        /// Status: Stable (default: false)
        /// </para>
        /// </summary>
        public bool HideCodeFirstEntitiesInTrees { get; set; }

        /// <summary>
        /// <para>
        /// Register built-in converters and data type instances for string, int, DateTime and bool types
        /// </para>
        /// <para>
        /// Status: Stable (default: true)
        /// </para>
        /// </summary>
        public bool UseBuiltInPrimitiveDataTypes { get; set; }

        /// <summary>
        /// <para>
        /// Add all known code-first media types as allowed children of the default Folder media type
        /// </para>
        /// <para>
        /// Status: Stable (default: true)
        /// </para>
        /// </summary>
        public bool AllowAllMediaTypesInDefaultFolder { get; set; }

        /// <summary>
        /// <para>
        /// Use the built-in code-first classes for the default media types - Image, File and Folder (do this if you don't need to modify those types)
        /// </para>
        /// <para>
        /// Status: Stable (default: true)
        /// </para>
        /// </summary>
        public bool UseBuiltInMediaTypes { get; set; }

        /// <summary>
        /// <para>
        /// Allow data types to access custom attributes from their model rendering ancestry (data type itself, current property, 
        /// current content type, current composing type, current composition property) when rendering. Currently used to allow
        /// CSS classes and data- attributes to be applied to emitted HTML elements, e.g. so all image elements have class="codefirst-image"
        /// or all emitted elements on a content type have data-mydata="myValue"
        /// </para>
        /// <para>
        /// HTML is emitted by any data type which implements IHtmlString, including many of the built-in ones, when the property is accessed
        /// within an HTML element body on a Razor view causing the Razor renderer to call ToHtmlString on the data type. 
        /// If the property is accessed within a HTML element tag (e.g. as an attribute value) then
        /// ToString() is called instead. In general the ToHtmlString method will render a sensible entire element (i.e. img element for an image, a element for a RelatedLink)
        /// where the ToString() method will render the most relevant bit of data (i.e. the image URL or the RelatedLink URL).
        /// </para>
        /// <para>
        /// Status: Experimental (default: false)
        /// </para>
        /// </summary>
        public bool UseTraversalAttributes { get; set; }

        /// <summary>
        /// <para>
        /// Enables multi-threaded timers which measure the performance (in terms of wall-clock time) of the core modules during initialisation.
        /// You can write a verbose output of the measurements to a folder of your choice by calling Diagnostics.Timing.SaveReport(_filePath); after
        /// initialisation, but *before* turning this feature off.
        /// </para>
        /// <para>
        /// It is recommended that you turn this feature on immediately before intialisation and off immediately after (after saving the report of course).
        /// </para>
        /// <para>
        /// Status: Stable (default: false)
        /// </para>
        /// </summary>
        public bool EnablePerformanceDiagnosticTimer
        {
            get
            {
                return Diagnostics.Timing.Enabled;
            }
            set
            {
                Diagnostics.Timing.Enabled = value;
            }
        }
    }
}