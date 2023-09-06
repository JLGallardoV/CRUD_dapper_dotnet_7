using UscastoresProject.Models;
using UscastoresProject.Service;

namespace UscastoresProject.controller;

class UscastoresController{

    public void MapRoutes(WebApplication app){

        app.MapGet("/Uscastores/Get", async (IUscastores service) =>
        {
            Console.WriteLine("ALL REQ");
            var result = await service.GetAll();
            return result.Any() ? Results.Ok(result) : Results.NotFound("No records found"); //revisamos si result contiene algun elemento despues del realizado de GetAll() si contiene resultados los regresamos caso contrario regresamos un 404
        });

        app.MapGet("/Uscastores/GetById", async (IUscastores service, int id) =>
        {
            Console.WriteLine($"BYID REQ {id}");
            var result = await service.GetById(id);
            return result is not null ? Results.Ok(result) : Results.NotFound($"No record found - id: {id}");
        });

        app.MapPost("/Uscastores/Create", async (IUscastores service, Uscastores colaborador) =>
        {
            Console.WriteLine("POST REQ");
            bool result = await service.Create(colaborador);
            return result ? Results.Ok() : Results.BadRequest("Error creating new record");
        });

        app.MapPut("/Uscastores/Update", async (IUscastores service, Uscastores colaborador) =>
        {
            Console.WriteLine("UPDATE REQ");
            bool result = await service.Update(colaborador);
            return result ? Results.Ok() : Results.NotFound($"No record found - id: {colaborador.IdPersonal}");
        });

        app.MapPost("/Uscastores/Upload", async (IUscastores service, IFormFile file) =>
        {
            Console.WriteLine("UPLOAD REQ");
            bool result = await service.Upload(file);
            return result ? Results.Ok("File uploaded successfully.") : Results.BadRequest("File no uploaded");

        });

        app.MapGet("/Uscastores/Download", (IUscastores service, string fileName) =>
        {
            Console.WriteLine($"Download REQ {fileName}");
            var filePath = service.Download(fileName).Result; // Use .Result to get the result synchronously.

            if (filePath == null)
            {
                return Results.NotFound("File not found.");
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return Results.File(fileBytes, "application/octet-stream", fileName);
        });

    }




}

