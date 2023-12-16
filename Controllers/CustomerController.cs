using Microsoft.AspNetCore.Mvc;
using Session4.DVM;
using Session4.Models;
using Microsoft.AspNetCore.Hosting;


namespace Session4.Controllers
{
    public class CustomerController : Controller
    {
        private readonly CLSDBContext _context;
        private readonly IWebHostEnvironment iWebHostEnvironment;

        public CustomerController(CLSDBContext context,IWebHostEnvironment iWebHostEnvironment) 
        {
            _context = context;
            this.iWebHostEnvironment = iWebHostEnvironment;
        }

        public IActionResult Index()
        {
             var custs=_context.Customers.ToList();
            return View(custs);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                //insert

                string fileName = null;
                if (model.Photo != null)
                {
                    string path = Path.Combine(iWebHostEnvironment.WebRootPath, "Images");
                    fileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(path, fileName);

                    using (var filestrem = new FileStream(filePath, FileMode.Create))
                    {
                        model.Photo.CopyTo(filestrem);
                    }

                }
                Customer c = new Customer();
                c.Name = model.Name;
                c.Email = model.Email;
                c.Phone = model.Phone;
                c.Address = model.Address;
                c.Image = fileName;

                _context.Customers.Add(c);
                _context.SaveChanges();
                return RedirectToAction("index");
            }
            return View(model);

        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Customer cust = _context.Customers.Find(id);
            if (cust==null)
            {
                Response.StatusCode = 500;
                ViewBag.ErrorMessage = "Error " + Response.StatusCode;
                return View("NotFound", id);
            }
            return View(cust);

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var cust=_context.Customers.Find(id);
            CustomerEditViewModel customerEditViewModel=new CustomerEditViewModel();
            customerEditViewModel.Id = cust.CustomerId;
            customerEditViewModel.Name = cust.Name;
            customerEditViewModel.Address = cust.Address;
            customerEditViewModel.Email=cust.Email;
            customerEditViewModel.Phone = cust.Phone;
            customerEditViewModel.ExistingPhotoPath = cust.Image;
            return View(customerEditViewModel);

        }

        [HttpPost]
        public IActionResult Edit(CustomerEditViewModel model)
        {


            if (ModelState.IsValid)
            {

                Customer customer = _context.Customers.Find(model.Id);
                customer.Name = model.Name;
                customer.Address= model.Address;
                customer.Email= model.Email;
                customer.Phone= model.Phone;

                string fileName = null;
                // customer choose new image(Photo ==>IFormFile)
                if (model.Photo!=null)
                {

                    //model.ExistingPhotoPath ==>old image name returned from database
                    if (model.ExistingPhotoPath != null)
                    {
                        // there are old Image
                        // i will delete the old image from Image Folder
                        string OldFilePath = Path.Combine(iWebHostEnvironment.WebRootPath + "\\Images\\" + model.ExistingPhotoPath);
                        System.IO.File.Delete(OldFilePath);

                    }
                    //

                    // upload new image
                    
                    if (model.Photo != null)
                    {
                        string path = Path.Combine(iWebHostEnvironment.WebRootPath, "Images");
                        fileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                        string filePath = Path.Combine(path, fileName);

                        using (var filestrem = new FileStream(filePath, FileMode.Create))
                        {
                            model.Photo.CopyTo(filestrem);
                        }

                    }





                }

                customer.Image = fileName;
                _context.Customers.Update(customer);
                _context.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(model);



        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var customer = _context.Customers.Find(id);
            return View(customer);
        }


        [HttpPost]
        public IActionResult Delete(Customer cust)
        {
            if (ModelState.IsValid)
            {
                var customer = _context.Customers.Find(cust.CustomerId);
                if (customer!=null)
                {
                    // delete old image
                    string OldFilePath = Path.Combine(iWebHostEnvironment.WebRootPath + "\\Images\\" + customer.Image);
                    System.IO.File.Delete(OldFilePath);

                }
                _context.Customers.Remove(customer);
                _context.SaveChanges();

                return RedirectToAction("index");
            }
            return View(cust);

        }





}
}
