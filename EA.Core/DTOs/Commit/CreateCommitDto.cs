public record CreateCommitDto
{
    public string Message{get;set;}
    public int RepositoryId{get;set;}
}