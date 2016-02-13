using Felinesoft.UmbracoCodeFirst;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Felinesoft.UmbracoCodeFirst.DataTypes;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.Extensions;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;

namespace UmbracoCodeFirst.GeneratedTypes
{
    [DocumentType(@"Event Detail", @"EventDetail", null, @"icon-calendar", false, false, @"")]
    [Template(true, "EventDetail", "EventDetail")]
    public class Eventdetail : Seopage
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Name", @"name", false, @"", 4, false)]
            public Textstring Name { get; set; }

            [ContentProperty(@"Image", @"image", false, @"", 0, false)]
            public LegacyMediaPicker Image { get; set; }

            [ContentProperty(@"Description", @"description", false, @"", 12, false)]
            public RichtextEditor Description { get; set; }

            [ContentProperty(@"Start Date", @"startDate", false, @"", 7, false)]
            public DatePickerWithTime Startdate { get; set; }

            [ContentProperty(@"End Date", @"endDate", false, @"", 8, false)]
            public DatePickerWithTime Enddate { get; set; }

            [ContentProperty(@"Price", @"price", false, @"", 5, false)]
            public Textstring Price { get; set; }

            [ContentProperty(@"Schedule", @"schedule", false, @"", 15, false)]
            public UmbracoCodeFirst.GeneratedTypes.ScheduleWizardExpress2014 Schedule { get; set; }

            [ContentProperty(@"Location", @"location", false, @"", 14, false)]
            public UmbracoCodeFirst.GeneratedTypes.GoogleMap Location { get; set; }

            [ContentProperty(@"Types", @"types", false, @"", 13, false)]
            public UmbracoCodeFirst.GeneratedTypes.EventTypePicker Types { get; set; }

            [ContentProperty(@"Snippet", @"snippet", false, @"", 3, false)]
            public Textstring Snippet { get; set; }

            [ContentProperty(@"Program", @"program", false, @"", 1, false)]
            public UmbracoCodeFirst.GeneratedTypes.ClassPicker Program { get; set; }

            [ContentProperty(@"Offer", @"offer", false, @"", 6, false)]
            public Textstring Offer { get; set; }

            [ContentProperty(@"Info Text", @"infoText", false, @"", 9, false)]
            public Textstring Infotext { get; set; }

            [ContentProperty(@"Info CTA Text", @"infoCtaText", false, @"", 10, false)]
            public Textstring Infoctatext { get; set; }

            [ContentProperty(@"Info CTA Url", @"infoCTAUrl", false, @"", 11, false)]
            public UmbracoCodeFirst.GeneratedTypes.UrlPicker Infoctaurl { get; set; }

            [ContentProperty(@"All Programs", @"allPrograms", false, @"", 2, false)]
            public TrueFalse Allprograms { get; set; }

        }
        public class GalleryTab : TabBase
        {
            [ContentProperty(@"Images", @"images", false, @"", 0, false)]
            public UmbracoCodeFirst.GeneratedTypes.MultipleMediaPicker Images { get; set; }

        }
        public class MoreEventsTab : TabBase
        {
            [ContentProperty(@"Event 1", @"moreEventsEvent1", false, @"", 1, false)]
            public UmbracoCodeFirst.GeneratedTypes.EventPicker Moreeventsevent1 { get; set; }

            [ContentProperty(@"Event 2", @"moreEventsEvent2", false, @"", 2, false)]
            public UmbracoCodeFirst.GeneratedTypes.EventPicker Moreeventsevent2 { get; set; }

            [ContentProperty(@"Event URL 1", @"eventUrl1", false, @"", 3, false)]
            public UmbracoCodeFirst.GeneratedTypes.UrlPicker Eventurl1 { get; set; }

            [ContentProperty(@"Event URL 2", @"eventUrl2", false, @"", 4, false)]
            public UmbracoCodeFirst.GeneratedTypes.UrlPicker Eventurl2 { get; set; }

        }
        public class RegistrationTab : TabBase
        {
            [ContentProperty(@"Registration Text", @"registrationText", false, @"", 0, false)]
            public Textstring Registrationtext { get; set; }

            [ContentProperty(@"Registration Close Date", @"registrationCloseDate", false, @"", 1, false)]
            public DatePickerWithTime Registrationclosedate { get; set; }

            [ContentProperty(@"Registration Link Instructor", @"registrationLinkInstructor", false, @"", 2, false)]
            public UmbracoCodeFirst.GeneratedTypes.UrlPicker Registrationlinkinstructor { get; set; }

            [ContentProperty(@"Registration link none Instructor", @"registrationLinkNoneInstructor", false, @"", 4, false)]
            public UmbracoCodeFirst.GeneratedTypes.UrlPicker Registrationlinknoneinstructor { get; set; }

            [ContentProperty(@"Registration Info Text", @"registrationInfoText", false, @"", 6, false)]
            public Textstring Registrationinfotext { get; set; }

            [ContentProperty(@"Registration Info CTA Text", @"registrationInfoCtaText", false, @"", 7, false)]
            public Textstring Registrationinfoctatext { get; set; }

            [ContentProperty(@"Registration Info CTA Url", @"registrationInfoCtaUrl", false, @"", 8, false)]
            public UmbracoCodeFirst.GeneratedTypes.UrlPicker Registrationinfoctaurl { get; set; }

            [ContentProperty(@"Registration Link Instructor Text", @"registrationLinkInstructorText", false, @"", 3, false)]
            public Textstring Registrationlinkinstructortext { get; set; }

            [ContentProperty(@"Registration Link None Instructor Text", @"registrationLinkNoneInstructorText", false, @"", 5, false)]
            public Textstring Registrationlinknoneinstructortext { get; set; }

        }

        [ContentTab(@"Content", 3)]
        public ContentTab Content { get; set; }

        [ContentTab(@"Gallery", 1)]
        public GalleryTab Gallery { get; set; }

        [ContentTab(@"More Events", 2)]
        public MoreEventsTab MoreEvents { get; set; }

        [ContentTab(@"Registration", 3)]
        public RegistrationTab Registration { get; set; }

        [ContentProperty(@"External ID", "externalID", false, @"", 0, false)]
        public Textstring Externalid { get; set; }

        [ContentProperty(@"Author Override", "authorOverride", false, @"", 1, false)]
        public UmbracoCodeFirst.GeneratedTypes.AuthorPicker Authoroverride { get; set; }
    }
}