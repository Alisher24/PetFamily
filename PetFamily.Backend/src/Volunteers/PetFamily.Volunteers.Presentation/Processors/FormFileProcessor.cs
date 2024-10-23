using Microsoft.AspNetCore.Http;
using PetFamily.Core.Dtos;

namespace PetFamily.Volunteers.Presentation.Processors;

public class FormFileProcessor : IAsyncDisposable
{
    private readonly List<UploadFileDto> _fileDtos = [];

    public List<UploadFileDto> Process(IFormFileCollection files)
    {
        foreach (var file in files)
        {
            var stream = file.OpenReadStream();
            var fileDto = new UploadFileDto(stream, file.FileName);
            _fileDtos.Add(fileDto);
        }

        return _fileDtos;
    }
    
    public async ValueTask DisposeAsync()
    {
        foreach (var file in _fileDtos)
        {
            await file.Stream.DisposeAsync();
        }
    }
}