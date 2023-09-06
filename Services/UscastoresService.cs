using UscastoresProject.Data;
using UscastoresProject.Models;
using UscastoresProject.Service;

namespace UscastoresProject.Service;
public class UscastoresService : IUscastores
{
    private readonly UscastoresRepository _repository;

    public UscastoresService(UscastoresRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Uscastores>> GetAll() =>
    await _repository.GetAll();

    public async Task<List<Uscastores>> GetById(int id) =>
    await _repository.GetById(id);

    public async Task<bool> Create(Uscastores colaborador) =>
    await _repository.Create(colaborador);
    
    public async Task<bool> Update(Uscastores colaborador) =>
    await _repository.Update(colaborador);

    public async Task<bool> Upload(IFormFile file)
    {
        if (file != null && file.Length > 0)
        {
            // Define the file path where you want to save it (directly)
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", file.FileName);

            // Ensure the "Uploads" directory exists
            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Uploads"));

            // Save the file to the specified path
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Handle the file upload success
            return true;
        }
        
     return false;
        
    }
    
    public Task<string> Download(string fileName)
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", fileName);

        if (System.IO.File.Exists(filePath))
        {
            return Task.FromResult(filePath); // Return a completed task with the filePath.
        }

        return Task.FromResult<string>(null); // Return a completed task with null when the file is not found.
    }

}