using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;

namespace utilities_biometric_seek.Extensions;

public static class FormFileExtensions
{
    public static async Task<string> ConvertToBase64(this IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);

        using var image = Image.Load(memoryStream.ToArray());
        image.Mutate(x => x.Resize(new ResizeOptions
        {
            Size = new Size(20, 20),
            Mode = ResizeMode.Max
        }));

        using var resizedMemoryStream = new MemoryStream();
        image.Save(resizedMemoryStream, new PngEncoder());
        byte[] resizedImageBytes = resizedMemoryStream.ToArray();

        return Convert.ToBase64String(resizedImageBytes);
    }
}
