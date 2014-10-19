using System;
using System.Collections.Generic;

namespace net.the_engineers.elmar.everywhere.Dto
{
    public class PersonDto : DtoEntity
    {
        public PersonDto()
        {
            PersonCompanies = new List<PersonCompanyDto>();
            WorksOnProjects = new List<PersonProjectDto>();
            EMailAddresses = new List<EMailAddressDto>();
            PhoneNumbers = new List<PhoneNumberDto>();
        }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTimeOffset? Birthday { get; set; }

        public List<PersonCompanyDto> PersonCompanies { get; set; }

        public List<PersonProjectDto> WorksOnProjects { get; set; }

        public List<EMailAddressDto> EMailAddresses { get; set; }
        public List<PhoneNumberDto> PhoneNumbers { get; set; }

        public string Fullname
        {
            get
            {
                if (Firstname == null && Lastname == null)
                    return "John Doe";

                if (Firstname == null)
                    return Lastname;

                if (Lastname == null)
                    return Firstname;

                return Firstname + " " + Lastname;

            }
        }
    }
}