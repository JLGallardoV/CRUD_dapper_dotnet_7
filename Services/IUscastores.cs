using UscastoresProject.Models;

namespace  UscastoresProject.Service;

public interface IUscastores
{
    Task<List<Uscastores>> GetAll();
    Task<List<Uscastores>> GetById(int id);
    Task<bool> Create(Uscastores colaborador);
    Task<bool> Update(Uscastores colaborador);
    Task<bool> Upload(IFormFile file);
    Task<string> Download(string file);

}


