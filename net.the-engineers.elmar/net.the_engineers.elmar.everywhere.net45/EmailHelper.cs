namespace net.the_engineers.elmar.everywhere.net45
{
    public class EmailHelper
    {
        public static string StripImpossibleChars(string email)
        {
            if (email != null) 
                return email.Replace("'", "");

            return email;
        }
    }
}
