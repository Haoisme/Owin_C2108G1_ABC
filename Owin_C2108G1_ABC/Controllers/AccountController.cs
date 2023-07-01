using Microsoft.AspNetCore.Mvc;
using Owin_C2108G1_ABC.Model;
using Owin_C2108G1_ABC.Data;
namespace Owin_C2108G1_ABC.Controllers
{
    [ApiController]

    public class AccountController : ControllerBase
    {
        private readonly DbConnection db;
        public AccountController(DbConnection db)
        {
            this.db = db;
        }
        [HttpGet]
        [Route("[controller]/[action]")]
        public IEnumerable<Account> get()
        {
            IEnumerable<Account> accounts = db.Account.ToList();
            return accounts;
        }
        [HttpPost]
        [Route("[controller]/[action]")]
        public IActionResult CreatNew(Account account)
        {
            Boolean flag = true;
            if (isDupplicated(account.Name))
            {
                ModelState.AddModelError("AccountDupplicated", "Account is dupplicated");
                flag = false;
            }
            if (!ModelState.IsValid)
            {
                flag = false;
                return Ok(ModelState);
            }
            if (flag)
            {
                db.Account.Add(account);
                db.SaveChanges();
            }
            return Ok(account);
        }



        [HttpPut]
        [Route("[controller]/[action]/{c}")]
        public List<Account> DeleteProduct(int c)
        {
            Account account = db.Account.Where(x => x.Id == c).First();
            db.Account.Remove(account);
            db.SaveChanges();
            return db.Account.ToList();
        }


        [HttpGet]
        [Route("[controller]/[action]/{c}")]
        public IEnumerable<Account> SearchByName(string c)
        {
            string[] x = c.Split(' ');
            List<Account> products = new List<Account>();
            List<Account> cate = new List<Account>();
            foreach (string s in x)
            {

                products = db.Account.Where(x => x.Name.ToLower().Contains(s.ToLower()))
                    .ToList();
                products.ForEach(x => cate.Add(x));
            }
            return cate;
        }




        [HttpGet]
        [Route("[controller]/[action]/{c}")]
        public IEnumerable<Account> SearchById(int c)
        {
            List<Account> products = new List<Account>();
            products = db.Account.Where(x => x.Id.Equals(c))
                .ToList();
            return products;
        }

        [HttpPut]
        [Route("[controller]/[action]/{Id}")]
        public List<Account> UpdateById(Account cate, int Id)
        {
            Account account = db.Account.Where(x => x.Id == Id).First();
            account.Name = cate.Name;
            account.Email = cate.Email;
            account.AccNo = cate.AccNo;
            db.SaveChanges();
            return db.Account.ToList();

        }
        [HttpGet]
        [Route("[controller]/[action]")]
        public Boolean isDupplicated(string accName)
        {
            Boolean flag = true;
            Account account = db.Account.Where(acc => acc.Name.ToLower().Equals(accName.ToLower())).FirstOrDefault();
            if (account is null)
            {
                flag = false;
            }
            return flag;
        }
    }
}

