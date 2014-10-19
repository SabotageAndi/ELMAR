namespace net.the_engineers.elmar.everywhere.Dto
{
    public class PersonProjectDto : DtoEntity
    {
        public PersonDto Person { get; set; }
        public ProjectDto Project { get; set; }
    }
}