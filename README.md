
# EntityRepositoryLibrary
### Respository Wrapper Librray for Entity Framework Core 
## Features
- Generic Entity Model Repositories
- Best works with Unit-Of-Work-Pattern
- Adds base funktionality to your Repositories
- Use Context in inherited Repositories
- Supports pageable requests
- Fully sync and async functions
- IEnumerable returns instad of IQueryable from Baserepository-Functions

# Dependencies
    net5.0
    Microsoft.EntityFrameworkCore (>= 5.0.8)

# Example
### Model
```csharp 
    public class Person
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        ...
    }
```
 ### Repository Interface
 ```csharp 
    public interface IPersonRepository : IRepository<Person>
    {
        IEnumberable<Person> GetPersonsOlderThen(int years)
    }
```
 ### Repository 
 ```csharp 
public class PersonRepository : Repository<Person>, IImageRepository
    {
        public ImageRepository(DbContext context) : base(context){}

        public IEnumerable<Image> GetPersonsOlderThen(int years)
          => Context<Context>
          .Persons
          .Where(person => person.Birthday.Year > DateTime.Now.Year - years)
          .ToList();
    }
```

 ### UnitOfWork 
 ```csharp 
 public class UnitOfWork : IUnitOfWork
    {
        private readonly PersonContext _context;
        public IPersonRepository Persons { get; private set; }
        
        public UnitOfWork(Context context)
        {
            _context = context;
            Persons = new PersonRepository(context);
        }

        public int Complete() => _context.SaveChanges();

        public async Task<int> CompleteAsync(CancellationToken cancellationToken = default)
            => await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
```

# Generic Base Repository Functions
### Sync
- Get
- GetAll
- Find
- Single
- SingleOrDefault
- First
- FirstOrDefault
- Add
- AddRange
- Remove
- RemoveRange
- GetPage
    
### Async
- GetAsync
- GetAllAsync
- FindAsync
- SingleAsync
- SingleOrDefaultAsync
- FirstAsync
- FirstOrDefaultAsync
- AddAsync
- AddRangeAsync
- RemoveAsync
- RemoveRangeAsync
- GetPageAsync
