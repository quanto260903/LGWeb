namespace LGWebsite
{
    public class Position
    {
        // Define additional static positions
        public static string Position_AboutUs_Top = "AboutUs_Top";
        public static string Position_AboutUs_Strategic = "AboutUs_Strategic";
        public static string Position_AboutUs_Ceo = "AboutUs_Ceo";
        public static string Position_AboutUs_Team = "AboutUs_Team";
        public static string Position_Careers_Top = "Careers_Top";
        public static string Position_Contact_RightTop = "Contact_RightTop";
        public static string Position_Contact_RightBottom = "Contact_RightBottom";
        public static string Position_Careers_Job= "Careers_Job_Opportunities";
        public static string Position_Home_Slide= "Home_Slide_Top";

        //quan
        public static string Position_Menu = "Menu";
        public static string Position_AgileModel = "Agile_Model";
        public static string Position_Appliedtools = "Appliedtools";
        public static string Position_OurExpertise = "Our_Expertise";
        public static string Position_Technologies = "Technologies";
        public static string Position_Home_Top = "Home_Top";
        public static string Position_ResourceScheduling = "Resource_Scheduling";
        public static string Position_DigitalSignage = "Digital_Signage";
        public static string Position_ExchangeOutlook = "Exchange_And_Outlook";

        // Get a list of all positions
        public static List<string> GetListPosition()
        {
            return new List<string>
            {
                Position_AboutUs_Top,
                Position_AboutUs_Strategic,
                Position_AboutUs_Ceo,
                Position_AboutUs_Team,
                Position_Careers_Job,
                Position_Careers_Top,
                Position_Contact_RightTop,
                Position_Contact_RightBottom,
                Position_AgileModel,
                Position_Appliedtools,
                Position_Technologies,
                Position_Home_Top,
                Position_Menu,
                Position_OurExpertise,
                Position_ResourceScheduling,
                Position_DigitalSignage,
                Position_ExchangeOutlook,
                Position_Home_Slide
            };
        }



    }


}
