namespace BalanceMaster.Domain.Models;

public class PagedResponse<T>
{
    public IEnumerable<T> Data { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
}

//var items = await _dbContext.Items
//    .Skip((page - 1) * pageSize)
//    .Take(pageSize)
//    .ToListAsync();

// public async Task<IActionResult> GetItems([FromQuery] int page = 1, [FromQuery] int pageSize = 10)

/*

 {
     "data": [
       {
         "id": 1,
         "name": "Item 1"
       },
       {
         "id": 2,
         "name": "Item 2"
       }
       // ...
     ],
     "meta": {
       "page": 1,
       "pageSize": 10,
       "totalItems": 45,
       "totalPages": 5
     },
     "links": {
       "self": "https://api.example.com/items?page=2&pageSize=10",
       "next": "https://api.example.com/items?page=3&pageSize=10",
       "prev": "https://api.example.com/items?page=1&pageSize=10"
     }
   }

 */