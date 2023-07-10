using Online_CV_Builder.Data.Entities;
using Online_CV_Builder.Data;
using Microsoft.EntityFrameworkCore;

namespace Online_CV_Builder.Configuration
{

    public static class UsersEndpoints
    {
        public static void MapUsersEndpoints(this IEndpointRouteBuilder routes, Users user)
        {
            routes.MapGet("/api/Users", async (ResumeBuilderContext db) =>
            {
                return await db.Users.ToListAsync();
            })
            .WithName("GetAllUserss")
            .Produces<List<Users>>(StatusCodes.Status200OK);

            routes.MapGet("/api/Users/{id}", async (int Id, ResumeBuilderContext db) =>
            {
                return await db.Users.FindAsync(Id)
                    is Users model
                        ? Results.Ok(model)
                        : Results.NotFound();
            })
            .WithName("GetUsersById")
            .Produces<Users>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            routes.MapPut("/api/Users/{id}", async (int Id, Users users, ResumeBuilderContext db) =>
            {
                var foundModel = await db.Users.FindAsync(Id);

                if (foundModel is null)
                {
                    return Results.NotFound();
                }

                db.Update(users);

                await db.SaveChangesAsync();

                return Results.NoContent();
            })
            .WithName("UpdateUsers")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status204NoContent);

            routes.MapPost("/api/Users/", async (Users users, ResumeBuilderContext db) =>
            {
                db.Users.Add(users);
                await db.SaveChangesAsync();
                return Results.Created($"/Userss/{users.Id}", users);
            })
            .WithName("CreateUsers")
            .Produces<Users>(StatusCodes.Status201Created);


            routes.MapDelete("/api/Users/{id}", async (int Id, ResumeBuilderContext db) =>
            {
                if (await db.Users.FindAsync(Id) is Users users)
                {
                    db.Users.Remove(users);
                    await db.SaveChangesAsync();
                    return Results.Ok(users);
                }

                return Results.NotFound();
            })
            .WithName("DeleteUsers")
            .Produces<Users>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
        }
    }
}

