namespace LearningSystem.Services.Interfaces
{
    public interface IPdfGenerator
    {
        byte[] GeneratePdfFromHtml(string html);
    }
}
