using TroyLabs.Models;
using Microsoft.EntityFrameworkCore;

namespace TroyLabs.Data;

// Creates a class called ApplicationDbContext that inherits from DbContext
// DbContext is a class from Entity Framework Core that allows you to interact with a database.
public class ApplicationDbContext: DbContext{
    // Constructor that takes in an instance of DbContextOptions and passes it to the base class constructor (DbContext)
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { 

    }

    // DbSet properties that represent tables in the database (Student, Teacher, Course, Classes, Device, Record) 
    public DbSet<Student> Students { get; set; }
    public DbSet<Survey> Surveys { get; set; }

}