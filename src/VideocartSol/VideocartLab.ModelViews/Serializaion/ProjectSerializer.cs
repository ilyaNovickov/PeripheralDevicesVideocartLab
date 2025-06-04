using System.Text.Json;
using System.Text.Json.Serialization;
using VideocartLab.ModelViews.Models;

namespace VideocartLab.ModelViews.Serializaion
{
    [Obsolete]
    public static class ProjectSerializer
    {
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            WriteIndented = true,
            // Если хочешь сериализовать object (InnerModel), стоит добавить:
            IncludeFields = true,
            ReferenceHandler = ReferenceHandler.Preserve,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };

        public static string Serialize(ProjectModel project)
        {
            return JsonSerializer.Serialize(project, _options);
        }

        public static ProjectModel? Deserialize(string json)
        {
            return JsonSerializer.Deserialize<ProjectModel>(json, _options);
        }

        // Для сохранения в файл
        public static async Task SerializeToFileAsync(ProjectModel project, string filePath)
        {
            using var stream = File.Create(filePath);
            await JsonSerializer.SerializeAsync(stream, project, _options);
        }

        // Для загрузки из файла
        public static async Task<ProjectModel?> DeserializeFromFileAsync(string filePath)
        {
            using var stream = File.OpenRead(filePath);
            return await JsonSerializer.DeserializeAsync<ProjectModel>(stream, _options);
        }
    }
}
