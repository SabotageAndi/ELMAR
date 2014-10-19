namespace net.the_engineers.elmar.everywhere.Dto
{
    public class PersonWorksOnProjectDto : DtoEntity
    {
        public PersonDto Person { get; set; }
        public ProjectDto Project { get; set; }
    }
}