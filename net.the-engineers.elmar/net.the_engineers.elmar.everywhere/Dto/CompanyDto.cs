using System.Collections.Generic;

namespace net.the_engineers.elmar.everywhere.Dto
{
    public class CompanyDto : DtoEntity
    {
        public CompanyDto()
        {
            PersonCompanies = new List<PersonCompanyDto>();
            CompanyProjects = new List<CompanyProjectDto>();
        }

        public string Name { get; set; }
        public List<PersonCompanyDto> PersonCompanies { get; set; }
        public List<CompanyProjectDto> CompanyProjects { get; set; }
    }
}