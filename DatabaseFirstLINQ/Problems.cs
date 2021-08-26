using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DatabaseFirstLINQ.Models;
using System.Collections.Generic;

namespace DatabaseFirstLINQ
{
    class Problems
    {
        private ECommerceContext _context;

        public Problems()
        {
            _context = new ECommerceContext();
        }
        public void RunLINQQueries()
        {
            //ProblemOne();
            //ProblemTwo();
            //ProblemThree();
            //ProblemFour();
            //ProblemFive();
            //ProblemSix();
            //ProblemSeven();
            //ProblemEight();
            //ProblemNine();
            //ProblemTen();
            //ProblemEleven();
            //ProblemTwelve();
            //ProblemThirteen();
            //ProblemFourteen();
            //ProblemFifteen();
            //ProblemSixteen();
            //ProblemSeventeen();
            //ProblemEighteen();
            //ProblemNineteen();
            //ProblemTwenty();
            // BonusOne();
            //BonusTwo();
            BonusThree();
        }

        // <><><><><><><><> R Actions (Read) <><><><><><><><><>
        private void ProblemOne()
        {
            // Write a LINQ query that returns the number of users in the Users table.
            // HINT: .ToList().Count
            int users = _context.Users.ToList().Count;
            Console.WriteLine(users);
        }

        private void ProblemTwo()
        {
            // Write a LINQ query that retrieves the users from the User tables then print each user's email to the console.
            var users = _context.Users;

            foreach (User user in users)
            {
                Console.WriteLine(user.Email);
            }

        }

        private void ProblemThree()
        {
            // Write a LINQ query that gets each product where the products price is greater than $150.
            // Then print the name and price of each product from the above query to the console.
            var productsLessThan150 = _context.Products.ToList().Where(x => x.Price > 150);
            foreach(var product in productsLessThan150)
            {
                Console.WriteLine($"{product.Name} {product.Price}");
            }

        }

        private void ProblemFour()
        {
            // Write a LINQ query that gets each product that contains an "s" in the products name.
            // Then print the name of each product from the above query to the console.
            var sInName = _context.Products.ToList().Where(s => s.Name.Contains('s') || s.Name.Contains('S')).Select(s => s.Name);
            foreach(var productNames in sInName)
            {
                Console.WriteLine(productNames);
            }

        }

        private void ProblemFive()
        {
            // Write a LINQ query that gets all of the users who registered BEFORE 2016
            // Then print each user's email and registration date to the console.
            DateTime compare = new DateTime(2016, 1, 1);
            var usersBefore2016 = _context.Users.ToList().Where(x => x.RegistrationDate < compare);

            foreach (var user in usersBefore2016)
            {
                Console.WriteLine($"{user.Email} {user.RegistrationDate}");
            }
        }

        private void ProblemSix()
        {
            // Write a LINQ query that gets all of the users who registered AFTER 2016 and BEFORE 2018
            // Then print each user's email and registration date to the console.
            DateTime after = new DateTime(2016, 12, 31);
            DateTime before = new DateTime(2018, 1, 1);

            var usersBefore2016 = _context.Users.ToList().Where(x => x.RegistrationDate < before && x.RegistrationDate > after);

            foreach (var user in usersBefore2016)
            {
                Console.WriteLine($"{user.Email} {user.RegistrationDate}");
            }

        }

        // <><><><><><><><> R Actions (Read) with Foreign Keys <><><><><><><><><>

        private void ProblemSeven()
        {
            // Write a LINQ query that retreives all of the users who are assigned to the role of Customer.
            // Then print the users email and role name to the console.
            var customerUsers = _context.UserRoles.Include(ur => ur.Role).Include(ur => ur.User).Where(ur => ur.Role.RoleName == "Customer");
            foreach (UserRole userRole in customerUsers)
            {
                Console.WriteLine($"Email: {userRole.User.Email} Role: {userRole.Role.RoleName}");
            }
        }

        private void ProblemEight()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "afton@gmail.com".
            // Then print the product's name, price, and quantity to the console.
            var aftonCart = _context.ShoppingCarts.Include(sc => sc.Product).Include(sc => sc.User).Where(sc => sc.User.Email == "afton@gmail.com");
            foreach (var product in aftonCart)
            {
                Console.WriteLine($"Product Name: {product.Product.Name} Price: {product.Product.Price} Quantity: {product.Quantity}");
            }
        }

        private void ProblemNine()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "oda@gmail.com" and returns the sum of all of the products prices.
            // HINT: End of query will be: .Select(sc => sc.Product.Price).Sum();
            // Then print the total of the shopping cart to the console.
            var odaCart = _context.ShoppingCarts.Include(sc => sc.Product).Include(sc => sc.User).Where(sc => sc.User.Email == "oda@gmail.com").Select(sc => (sc.Product.Price * (decimal)sc.Quantity)).Sum();
            Console.WriteLine("Sum of Oda's Cart: $" + odaCart);

        }

        private void ProblemTen()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of users who have the role of "Employee".
            // Then print the user's email as well as the product's name, price, and quantity to the console.
            var employees = _context.UserRoles.Include(ur => ur.Role).Include(ur => ur.User).Where(ur => ur.Role.RoleName == "Employee").ToList();
            foreach (var employee in employees)
            {
                // Get employee's cart
                var employeeCart = _context.ShoppingCarts.Include(sc => sc.Product).Include(sc => sc.User).Where(sc => sc.User.Email == employee.User.Email).ToList();

                // Print cart info
                foreach (var item in employeeCart)
                {
                    Console.WriteLine($"Employee Email: {employee.User.Email} Product Name: {item.Product.Name} Price: {item.Product.Price} Quantity: {item.Quantity}");
                }
            }
        }

        // <><><><><><><><> CUD (Create, Update, Delete) Actions <><><><><><><><><>

        // <><> C Actions (Create) <><>

        private void ProblemEleven()
        {
            // Create a new User object and add that user to the Users table using LINQ.
            User newUser = new User()
            {
                Email = "david@gmail.com",
                Password = "DavidsPass123"
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        private void ProblemTwelve()
        {
            // Create a new Product object and add that product to the Products table using LINQ.
            Product newProduct = new Product()
            {
                Name = "LG TV",
                Description = "Led tv in 4K.",
                Price = 200.00m,
            };
            _context.Products.Add(newProduct);
            _context.SaveChanges();

        }

        private void ProblemThirteen()
        {
            // Add the role of "Customer" to the user we just created in the UserRoles junction table using LINQ.
            var roleId = _context.Roles.Where(r => r.RoleName == "Customer").Select(r => r.Id).SingleOrDefault();
            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();
            UserRole newUserRole = new UserRole()
            {
                UserId = userId,
                RoleId = roleId
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        private void ProblemFourteen()
        {
            // Add the product you created to the user we created in the ShoppingCart junction table using LINQ.
            var productId = _context.Products.Where(r => r.Name == "LG TV").Select(r => r.Id).SingleOrDefault();
            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();
            ShoppingCart newCart = new ShoppingCart()
            {
                UserId = userId,
                ProductId = productId,
                Quantity = 1
            };
            _context.ShoppingCarts.Add(newCart);
            _context.SaveChanges();
            Console.WriteLine("Added item");
        }

        // <><> U Actions (Update) <><>

        private void ProblemFifteen()
        {
            // Update the email of the user we created to "mike@gmail.com"
            var user = _context.Users.Where(u => u.Email == "david@gmail.com").SingleOrDefault();
            user.Email = "mike@gmail.com";
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        private void ProblemSixteen()
        {
            // Update the price of the product you created to something different using LINQ.
            var product = _context.Products.Where(p => p.Name == "LG TV").SingleOrDefault();
            product.Price = 155m;
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        private void ProblemSeventeen()
        {
            // Change the role of the user we created to "Employee"
            // HINT: You need to delete the existing role relationship and then create a new UserRole object and add it to the UserRoles table
            // See problem eighteen as an example of removing a role relationship
            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "mike@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            UserRole newUserRole = new UserRole()
            {
                UserId = _context.Users.Where(u => u.Email == "mike@gmail.com").Select(u => u.Id).SingleOrDefault(),
                RoleId = _context.Roles.Where(r => r.RoleName == "Employee").Select(r => r.Id).SingleOrDefault()
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        // <><> D Actions (Delete) <><>

        private void ProblemEighteen()
        {
            // Delete the role relationship from the user who has the email "oda@gmail.com" using LINQ.
            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "oda@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            _context.SaveChanges();
        }

        private void ProblemNineteen()
        {
            // Delete all of the product relationships to the user with the email "oda@gmail.com" in the ShoppingCart table using LINQ.
            // HINT: Loop
            var shoppingCartProducts = _context.ShoppingCarts.Where(sc => sc.User.Email == "oda@gmail.com");
            foreach (ShoppingCart userProductRelationship in shoppingCartProducts)
            {
                _context.ShoppingCarts.Remove(userProductRelationship);
            }
            _context.SaveChanges();
        }

        private void ProblemTwenty()
        {
            // Delete the user with the email "oda@gmail.com" from the Users table using LINQ.
            var deletedUser = _context.Users.Where(u => u.Email == "oda@gmail.com").SingleOrDefault();
            _context.Users.Remove(deletedUser);
            _context.SaveChanges();

        }

        // <><><><><><><><> BONUS PROBLEMS <><><><><><><><><>

        private void BonusOne()
        {
            // Prompt the user to enter in an email and password through the console.
            // Take the email and password and check if the there is a person that matches that combination.
            // Print "Signed In!" to the console if they exists and the values match otherwise print "Invalid Email or Password.".
            Console.Write("Enter your email: ");
            string user = Console.ReadLine();
            Console.Write("\nEnter your password: ");
            string pass = Console.ReadLine();

            var customer = _context.Users.Where(x => (x.Email == user && x.Password == pass)).FirstOrDefault();

            if (customer != null)
            {
                Console.WriteLine("Signed In!");
            }
            else
            {
                Console.WriteLine("Invalid Email or Password.");
            }
        }

        private void BonusTwo()
        {
            // Write a query that finds the total of every users shopping cart products using LINQ.
            // Display the total of each users shopping cart as well as the total of the totals to the console.
            var users = _context.Users.ToList();
            decimal totals = 0;

            foreach (var user in users)
            {
                decimal cartTotal = _context.ShoppingCarts.Include(sc => sc.Product).Include(sc => sc.User).Where(ct => ct.User.Email == user.Email).Select(sc => (sc.Product.Price * (decimal)sc.Quantity)).Sum();

                Console.WriteLine(user.Email + "$" + cartTotal);
                totals += cartTotal;
            }

            Console.WriteLine("Entire Shopping Cart Total: $" + totals);

        }

        // BIG ONE
        private void BonusThree()
        {
            // 1. Create functionality for a user to sign in via the console
            // 2. If the user succesfully signs in
            //    a. Give them a menu where they perform the following actions within the console
            //           View the products in their shopping cart
            //           Remove a product from their shopping cart
            //           View all products in the Products table
            //           Add a product to the shopping cart (incrementing quantity if that product is already in their shopping cart)
            // 3. If the user does not succesfully sign in
            //    a. Display "Invalid Email or Password"
            //    b. Re-prompt the user for credentials

            bool loggedIn = false;
            User customer;

            while (!loggedIn)
            {
                Console.Write("Enter your email: ");
                string user = Console.ReadLine();
                Console.Write("\nEnter your password: ");
                string pass = Console.ReadLine();

                customer = _context.Users.Where(x => (x.Email == user && x.Password == pass)).FirstOrDefault();

                if (customer != null)
                {
                    Console.WriteLine("Signed In!");
                    loggedIn = true;
                    
                    string userSelection = "";
                    while (userSelection != "0")
                    {
                        // Display menu
                        userSelection = Menu();
                        switch (userSelection)
                        {
                            case "1":
                                SeeShoppingCart(customer);
                                break;
                            case "2":
                                SeeProducts(customer);
                                break;
                            case "0":
                                Console.WriteLine("Sign up for our hourly newsletter for discounts you'll never use or want!!!");
                                break;
                            default:
                                break;
                        }


                        // Submenus - Cart, products
                        // Cart: View, remove items
                        // Products: View, add to cart

                    }
                }
                else
                {
                    Console.WriteLine("Invalid Email or Password.");
                }
            }


            
            

            // get response back, do something else with that choice

            // Loop until the exit

        }

        private void SeeShoppingCart(User customer)
        {

            // Submenu
            bool validSelection = false;
            string userSelection = "";

            while (!validSelection)
            {
                List<ShoppingCart> customerCart = _context.ShoppingCarts.Include(sc => sc.Product).Include(sc => sc.User).Where(sc => sc.User.Email == customer.Email).ToList();
                decimal cartTotal = _context.ShoppingCarts.Include(sc => sc.Product).Include(sc => sc.User).Where(sc => sc.User.Email == customer.Email).Select(sc => (sc.Product.Price * (decimal)sc.Quantity)).Sum();

                Console.WriteLine("Here is your shopping cart: ");
                foreach (var product in customerCart)
                {
                    Console.WriteLine($"\n\tProduct: {product.Product.Name} Price: {product.Product.Price} Quantity: {product.Quantity}");
                }
                Console.WriteLine($"Current Total: {cartTotal}");
                
                // Print options
                Console.WriteLine("\t1) Back to main menu\n\t2) Remove item from cart");
                // Wait for user input and validate
                userSelection = Console.ReadLine();
                validSelection = ValidateSelection(userSelection, "12");
                if (validSelection)
                {
                    if (userSelection == "1")
                    {
                        //Back
                        return;
                    }
                    else if (userSelection == "2")
                    {
                        //Remove from cart
                        RemoveFromCart(customerCart);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid selection. Please select a valid option from the list below:");
                }
            }
        }

        private void RemoveFromCart(List<ShoppingCart> cart)
        {
            string userSelection = "";
            while (userSelection != "0")
            {
                // Build out the menu
                List<string> validOptions = new List<string>() { "0" };
                for (int i = 1; i <= cart.Count; i++)
                {
                    validOptions.Add(i.ToString());
                }
                // Display menu
                for (int i = 0; i < cart.Count; i++)
                {
                    Console.WriteLine($"\t{i + 1}) {cart[i].Product.Name}");
                }
                Console.WriteLine("\t0) Return to shopping cart");
                // Get the input
                userSelection = Console.ReadLine();

                // Validate
                bool validSelection = validOptions.Contains(userSelection);

                // if Valid selection Remove from cart
                if (validSelection)
                {
                    if (userSelection == "0")
                    {
                        // Exit function, go back to cart
                        return;
                    }
                    else
                    {
                        // remove item from cart
                        int itemIndex = Int16.Parse(userSelection) - 1;
                        ShoppingCart itemToRemove = cart[itemIndex];
                        _context.ShoppingCarts.Remove(itemToRemove);
                        _context.SaveChanges();
                        return;
                    }
                }
            }
            return;
        }

        private void SeeProducts(User customer)
        {
            List<Product> allProducts = _context.Products.ToList();
            // menu
            bool validSelection = false;
            string userSelection = "";

            while (!validSelection)
            {
                Console.WriteLine("These products are avaiable: ");
                foreach (var product in _context.Products.ToList())
                {
                    Console.WriteLine($"\n\tProduct: {product.Name} Price: {product.Price}");
                }

                // Print options
                Console.WriteLine("\t1) Back to main menu\n\t2) Add item to cart");
                // Wait for user input and validate
                userSelection = Console.ReadLine();
                validSelection = ValidateSelection(userSelection, "12");
                if (validSelection)
                {
                    if (userSelection == "1")
                    {
                        //Back
                        return;
                    }
                    else if (userSelection == "2")
                    {
                        //Remove from cart
                        AddToCart(customer);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid selection. Please select a valid option from the list below:");
                }
            }
        }

        private void AddToCart(User customer)
        {
            string userSelection = "";
            while (userSelection != "0")
            {
                // Build out the menu
                List<Product> allProducts = _context.Products.ToList();
                List<string> validOptions = new List<string>() { "0" };
                for (int i = 1; i <= allProducts.Count; i++)
                {
                    validOptions.Add(i.ToString());
                }
                // Display menu
                for (int i = 0; i < allProducts.Count; i++)
                {
                    Console.WriteLine($"\t{i + 1}) {allProducts[i].Name}");
                }
                Console.WriteLine("\t0) Return to catalogue");
                // Get the input
                userSelection = Console.ReadLine();

                // Validate
                bool validSelection = validOptions.Contains(userSelection);

                // if Valid selection Remove from cart
                if (validSelection)
                {
                    if (userSelection == "0")
                    {
                        // Exit function, go back to cart
                        return;
                    }
                    else
                    {
                        // remove item from cart
                        int itemIndex = Int16.Parse(userSelection) - 1;
                        Product itemToAdd = allProducts[itemIndex];
                        ShoppingCart newCart = new ShoppingCart()
                        {
                            UserId = customer.Id,
                            ProductId = itemToAdd.Id,
                            Quantity = 1
                        };


                        _context.ShoppingCarts.Add(newCart);
                        _context.SaveChanges();
                        return;
                    }
                }
            }
            return;
        }

        private string Menu()
        {
            bool validSelection = false;
            string userSelection = "";
            Console.WriteLine("Welcome to the store!\nPlease select an option below:");

            while (!validSelection)
            {
                // Print options
                Console.WriteLine("\t1) See shopping cart\n\t2) See products\n\n\t0) Exit");
                // Wait for user input and validate
                userSelection = Console.ReadLine();
                validSelection = ValidateSelection(userSelection, "120");
                if (validSelection)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid selection. Please select a valid option from the list below:");
                }
            }
            return userSelection;
        }

        private bool ValidateSelection(string selection, string options)
        {
            if (options.Contains(selection) && selection.Length == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
