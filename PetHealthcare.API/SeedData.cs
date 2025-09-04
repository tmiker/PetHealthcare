using Microsoft.AspNetCore.Identity;
using PetHealthcare.DataAccess.Data;
using PetHealthcare.Domain.Models;
using System.Security.Claims;

namespace PetHealthcare.API
{
    public class SeedData
    {
        private static List<Pet> _pets = new List<Pet>()
        {
            new Pet { Name = "Wendy", Gender = "Female", Breed = "Chi-Princess", DateOfBirth = DateTime.Parse("2016-12-02"), DateOfAdoption = DateTime.Parse("2018-12-02"), ChipNumber = "981020025911645", Allergies = null, ImageURL = null, OwnedBy = "6de17564-15cd-412f-9804-537b7cbeb4c8" },
                new Pet { Name = "Marlow", Gender = "Male", Breed = "Chi-Raptor", DateOfBirth = DateTime.Parse("2016-06-04"), DateOfAdoption = DateTime.Parse("2018-07-22"), ChipNumber = "985112004775656", Allergies = null, ImageURL = null, OwnedBy = "6de17564-15cd-412f-9804-537b7cbeb4c8" }

        };

        private static List<Vet> _vets = new List<Vet>()
        {
            new Vet { Hospital = "Unspecified", Doctor = "Unspecified", Phone = "phone", Street1 = "Street 1", Street2 = "Street 2", City = "City", State = "State", ZipCode = "Zip Code", ImageBytes = null, OwnedBy = "6de17564-15cd-412f-9804-537b7cbeb4c8" },
                new Vet { Hospital = "Austin Pets Alive, Inc.", Doctor = "Shelby Asquith", Phone = "phone", Street1 = "Street 1", Street2 = "Street 2", City = "City", State = "State", ZipCode = "Zip Code", ImageBytes = null, OwnedBy = "6de17564-15cd-412f-9804-537b7cbeb4c8" },
                new Vet { Hospital = "Austin Pets Alive, Inc.", Doctor = "Kristina Bevers", Phone = "phone", Street1 = "Street 1", Street2 = "Street 2", City = "City", State = "State", ZipCode = "Zip Code", ImageBytes = null, OwnedBy = "6de17564-15cd-412f-9804-537b7cbeb4c8" },
                new Vet { Hospital = "Austin Pets Alive, Inc.", Doctor = "Annie Hoelle", Phone = "phone", Street1 = "Street 1", Street2 = "Street 2", City = "City", State = "State", ZipCode = "Zip Code", ImageBytes = null, OwnedBy = "6de17564-15cd-412f-9804-537b7cbeb4c8" },
                new Vet { Hospital = "VCA Arbor Animal Hospital", Doctor = "Jenette Lucia", Phone = "(512) 782-0374", Street1 = "5114 Balcones Woods Dr", Street2 = "Suite 312", City = "Austin", State = "TX", ZipCode = "78759", ImageBytes = null, OwnedBy = "6de17564-15cd-412f-9804-537b7cbeb4c8" },
                new Vet { Hospital = "VCA Arbor Animal Hospital", Doctor = "Jennifer Renner", Phone = "(512) 782-0374", Street1 = "5114 Balcones Woods Dr", Street2 = "Suite 312", City = "Austin", State = "TX", ZipCode = "78759", ImageBytes = null, OwnedBy = "6de17564-15cd-412f-9804-537b7cbeb4c8" }

        };

        private static List<Visit> _visits = new List<Visit>()
        {
            new Visit { PetId = 1, VetId = 1, DateOfVisit = DateTime.Parse("2018-05-25"), VisitType = "Shelter Care", Reason = "Shelter Initial Evaluation", Weight = 13.0, Diagnosis = "Heartworm Positive", Prescriptions = "Heartworm (oral), Flea and Tick (topical)", Instructions = "None", Notes = "None", Heartworm = false, Bordatella = true, Rabies = false, Da2ppv = false, Leptospirosis = false, InfluenzaH3N2 = false, InfluenzaH3N8 = false, FecalTest = false, OtherTests = "SNAP Heartworm Test", OwnedBy = "6de17564-15cd-412f-9804-537b7cbeb4c8" },
            new Visit { PetId = 1, VetId = 1, DateOfVisit = DateTime.Parse("2018-06-08"), VisitType = "Shelter Care", Reason = "Shelter Care: DHPP Shot", Weight = 13.0, Diagnosis = "None", Prescriptions = "None", Instructions = "None", Notes = "None", Heartworm = false, Bordatella = false, Rabies = false, Da2ppv = true, Leptospirosis = false, InfluenzaH3N2 = false, InfluenzaH3N8 = false, FecalTest = false, OtherTests = "None", OwnedBy = "6de17564-15cd-412f-9804-537b7cbeb4c8" },
            new Visit { PetId = 1, VetId = 1, DateOfVisit = DateTime.Parse("2018-06-10"), VisitType = "Shelter Care", Reason = "Shelter Care: Deworming", Weight = 13.0, Diagnosis = "None", Prescriptions = "Pyrantel Pamoate 50mg/mL 1 x daily", Instructions = "See prescription", Notes = "Deworming prescription", Heartworm = false, Bordatella = false, Rabies = false, Da2ppv = false, Leptospirosis = false, InfluenzaH3N2 = false, InfluenzaH3N8 = false, FecalTest = false, OtherTests = "None", OwnedBy = "6de17564-15cd-412f-9804-537b7cbeb4c8" },
            new Visit { PetId = 1, VetId = 2, DateOfVisit = DateTime.Parse("2018-06-15"), VisitType = "Shelter Care", Reason = "Austin Pets Alive Initial Care", Weight = 13.0, Diagnosis = "None", Prescriptions = "None", Instructions = "None", Notes = "Terrified. Flea dirty. Lean. Quiet, Alert, Responsive.", Heartworm = false, Bordatella = false, Rabies = false, Da2ppv = false, Leptospirosis = false, InfluenzaH3N2 = false, InfluenzaH3N8 = false, FecalTest = false, OtherTests = "Intake Exam", OwnedBy = "6de17564-15cd-412f-9804-537b7cbeb4c8" },
            new Visit { PetId = 1, VetId = 4, DateOfVisit = DateTime.Parse("2018-06-23"), VisitType = "Shelter Care", Reason = "DAPPv shot", Weight = 13.0, Diagnosis = "None", Prescriptions = "None", Instructions = "None", Notes = "None", Heartworm = false, Bordatella = false, Rabies = false, Da2ppv = false, Leptospirosis = false, InfluenzaH3N2 = false, InfluenzaH3N8 = false, FecalTest = false, OtherTests = "None", OwnedBy = "6de17564-15cd-412f-9804-537b7cbeb4c8" },
            new Visit { PetId = 1, VetId = 3, DateOfVisit = DateTime.Parse("2018-07-10"), VisitType = "Shelter Care", Reason = "Distemper Clearance Exam", Weight = 13.0, Diagnosis = "Cleared", Prescriptions = "None", Instructions = "None", Notes = "Cleared for Distemper. Was in foster care for 3-4 weeks. No sign of GI or URI. Bright Alert Responsive. Very nervous.", Heartworm = false, Bordatella = false, Rabies = false, Da2ppv = false, Leptospirosis = false, InfluenzaH3N2 = false, InfluenzaH3N8 = false, FecalTest = false, OtherTests = "None", OwnedBy = "6de17564-15cd-412f-9804-537b7cbeb4c8" },
            new Visit { PetId = 1, VetId = 4, DateOfVisit = DateTime.Parse("2018-07-22"), VisitType = "Shelter Care", Reason = "Spay", Weight = 13.0, Diagnosis = "None", Prescriptions = "Tramodol as needed", Instructions = "See prescription", Notes = "Routine ovariohysterectomy", Heartworm = false, Bordatella = false, Rabies = false, Da2ppv = false, Leptospirosis = false, InfluenzaH3N2 = false, InfluenzaH3N8 = false, FecalTest = false, OtherTests = "None", OwnedBy = "6de17564-15cd-412f-9804-537b7cbeb4c8" },
            new Visit { PetId = 2, VetId = 5, DateOfVisit = DateTime.Parse("2018-07-23"), VisitType = "Annual Exam", Reason = "Initial Exam", Weight = 9.4, Diagnosis = "Heartworm Negative", Prescriptions = "Heartgard Plus K9 S 1-25lb, Nexgard K9 Blue Md 1-24lb", Instructions = "None", Notes = "First visit post-adoption. Estimate 1 year old = Birthday 23 Jul 2017.", Heartworm = false, Bordatella = true, Rabies = true, Da2ppv = true, Leptospirosis = true, InfluenzaH3N2 = true, InfluenzaH3N8 = true, FecalTest = true, OtherTests = "Heartworm", OwnedBy = "6de17564-15cd-412f-9804-537b7cbeb4c8" },
            new Visit { PetId = 2, VetId = 5, DateOfVisit = DateTime.Parse("2018-08-13"), VisitType = "Shots Due", Reason = "Shot booster update", Weight = 9.4, Diagnosis = "None", Prescriptions = "None", Instructions = "None", Notes = "Follow up shots for initial exam.", Heartworm = false, Bordatella = false, Rabies = false, Da2ppv = true, Leptospirosis = false, InfluenzaH3N2 = true, InfluenzaH3N8 = true, FecalTest = true, OtherTests = "None", OwnedBy = "6de17564-15cd-412f-9804-537b7cbeb4c8" },
            new Visit { PetId = 2, VetId = 5, DateOfVisit = DateTime.Parse("2018-09-07"), VisitType = "Routine", Reason = "Health Certificate Domestic", Weight = 9.5, Diagnosis = "None", Prescriptions = "None", Instructions = "None", Notes = "Clearance to fly", Heartworm = false, Bordatella = false, Rabies = false, Da2ppv = false, Leptospirosis = false, InfluenzaH3N2 = false, InfluenzaH3N8 = false, FecalTest = false, OtherTests = "None", OwnedBy = "6de17564-15cd-412f-9804-537b7cbeb4c8" },
            new Visit { PetId = 1, VetId = 4, DateOfVisit = DateTime.Parse("2018-12-02"), VisitType = "Routine", Reason = "Adoption", Weight = 13.0, Diagnosis = "None", Prescriptions = "None", Instructions = "Heartworm Treatment Planning", Notes = "Adoption Day", Heartworm = false, Bordatella = false, Rabies = false, Da2ppv = false, Leptospirosis = false, InfluenzaH3N2 = false, InfluenzaH3N8 = false, FecalTest = false, OtherTests = "None", OwnedBy = "6de17564-15cd-412f-9804-537b7cbeb4c8" },
            new Visit { PetId = 2, VetId = 5, DateOfVisit = DateTime.Parse("2018-12-11"), VisitType = "Minor Incident", Reason = "Minor dog bite", Weight = 9.9, Diagnosis = "No breaks", Prescriptions = "Cefpodoxime 100mg, Rovera (Carprofen) 25mg", Instructions = "None", Notes = "X-rays and Cleanup", Heartworm = false, Bordatella = false, Rabies = false, Da2ppv = false, Leptospirosis = false, InfluenzaH3N2 = false, InfluenzaH3N8 = false, FecalTest = false, OtherTests = "None", OwnedBy = "6de17564-15cd-412f-9804-537b7cbeb4c8" },
            new Visit { PetId = 1, VetId = 6, DateOfVisit = DateTime.Parse("2018-12-20"), VisitType = "Annual Exam", Reason = "Initial VCA Arbor Exam", Weight = 11.5, Diagnosis = "None", Prescriptions = "Heartgard Plus K9 S 1-25lb, Nexgard K9 Blue Md 10.1-24lb", Instructions = "None", Notes = "None", Heartworm = false, Bordatella = false, Rabies = false, Da2ppv = false, Leptospirosis = false, InfluenzaH3N2 = false, InfluenzaH3N8 = false, FecalTest = false, OtherTests = "None", OwnedBy = "6de17564-15cd-412f-9804-537b7cbeb4c8" },
            new Visit { PetId = 1, VetId = 6, DateOfVisit = DateTime.Parse("2018-12-29"), VisitType = "Routine", Reason = "Radiograph Thorax", Weight = 11.1, Diagnosis = "Slight enlargement", Prescriptions = "Prednisolone 5mg", Instructions = "No decongestants or fever reducers (toxic)", Notes = "None", Heartworm = false, Bordatella = false, Rabies = false, Da2ppv = false, Leptospirosis = false, InfluenzaH3N2 = false, InfluenzaH3N8 = false, FecalTest = false, OtherTests = "None", OwnedBy = "6de17564-15cd-412f-9804-537b7cbeb4c8" },
            new Visit { PetId = 2, VetId = 6, DateOfVisit = DateTime.Parse("2020-10-08"), VisitType = "Annual Exam", Reason = "Annual Exam", Weight = 11.3, Diagnosis = "Healthy", Prescriptions = "Flea and Tick (oral)", Instructions = "None", Notes = "None", Heartworm = true, Bordatella = true, Rabies = false, Da2ppv = false, Leptospirosis = true, InfluenzaH3N2 = false, InfluenzaH3N8 = false, FecalTest = false, OtherTests = "Heartworm", OwnedBy = "6de17564-15cd-412f-9804-537b7cbeb4c8" },
            new Visit { PetId = 1, VetId = 6, DateOfVisit = DateTime.Parse("2020-10-08"), VisitType = "Annual Exam", Reason = "Annual Exam", Weight = 12.8, Diagnosis = "Healthy", Prescriptions = "Flea and Tick (oral)", Instructions = "None", Notes = "None", Heartworm = true, Bordatella = true, Rabies = false, Da2ppv = false, Leptospirosis = true, InfluenzaH3N2 = false, InfluenzaH3N8 = false, FecalTest = false, OtherTests = "Heartworm", OwnedBy = "6de17564-15cd-412f-9804-537b7cbeb4c8" }
        };
        public async static Task SeedPetHealthcareData(WebApplication app, IConfiguration config)
        {
            Console.WriteLine($"Seeding Pets and Visits ...");

            using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Pets.AddRange(_pets);
                context.Vets.AddRange(_vets);
                await context.SaveChangesAsync();

                context.Visits.AddRange(_visits);
                await context.SaveChangesAsync();
            }

            Console.WriteLine($"Seeding completed.");
        }

        public async static Task SeedUserRoles(WebApplication app, IConfiguration config)
        {
            Console.WriteLine("Seeding user roles ...");
            using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                if (!await roleMgr.RoleExistsAsync("Admin")) await roleMgr.CreateAsync(new IdentityRole("Admin"));
                if (!await roleMgr.RoleExistsAsync("Manager")) await roleMgr.CreateAsync(new IdentityRole("Manager"));
                if (!await roleMgr.RoleExistsAsync("Employee")) await roleMgr.CreateAsync(new IdentityRole("Employee"));
                if (!await roleMgr.RoleExistsAsync("Customer")) await roleMgr.CreateAsync(new IdentityRole("Customer"));
            }
        }

        public async static Task SeedAdminData(WebApplication app, IConfiguration config)
        {
            string? adminPassword = config.GetSection("SeedDataConfiguration").GetValue<string>("AdminSeedPassword");
            if (string.IsNullOrWhiteSpace(adminPassword))
            {
                Console.WriteLine("AdminSeedPassword not configured. Set in User Secrets.");
                return;
            }

            string? mikePassword = config.GetSection("SeedDataConfiguration").GetValue<string>("MikeSeedPassword");
            if (string.IsNullOrWhiteSpace(adminPassword))
            {
                Console.WriteLine("AdminSeedPassword not configured. Set in User Secrets.");
                return;
            }

            Console.WriteLine("Seeding admin user ...");
            using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                // context.Database.Migrate();

                var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var admin = userMgr.FindByNameAsync("admin").Result;
                if (admin == null)
                {
                    admin = new ApplicationUser
                    {
                        // don't seed identity column - Id = "d2bd63f7-abfb-4873-8e01-b4bac6ba2ba7",
                        UserName = "admin",
                        Email = "admin@somemail.com",
                        EmailConfirmed = true,
                        CustomerNumber = 486180454,
                        NormalizedUserName = "ADMIN",
                        NormalizedEmail = "ADMIN@SOMEMAIL.COM",
                        LockoutEnabled = true
                    };
                    IdentityResult adminResult = userMgr.CreateAsync(admin, adminPassword).Result;
                    if (!adminResult.Succeeded) throw new Exception(adminResult.Errors.First().Description);

                    adminResult = userMgr.AddClaimsAsync(admin, new Claim[]{
                            new Claim(ClaimTypes.Name, "Admin")
                    }).Result;
                    if (!adminResult.Succeeded) throw new Exception(adminResult.Errors.First().Description);

                    var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    if (!await roleMgr.RoleExistsAsync("Admin")) await roleMgr.CreateAsync(new IdentityRole("Admin"));
                    adminResult = await userMgr.AddToRoleAsync(admin, "Admin");
                    if (!adminResult.Succeeded) throw new Exception(adminResult.Errors.First().Description);
                    await userMgr.UpdateAsync(admin);

                    Console.WriteLine("admin user successfully created.");
                }
                else
                {
                    Console.WriteLine("admin already exists.");
                }
            }
        }

        public async static Task SeedUserData(WebApplication app, IConfiguration config)
        {
            string? mikePassword = config.GetSection("SeedDataConfiguration").GetValue<string>("MikeSeedPassword");
            if (string.IsNullOrWhiteSpace(mikePassword))
            {
                Console.WriteLine("MikeSeedPassword not configured. Set in User Secrets.");
                return;
            }

            Console.WriteLine("Seeding user mike ...");
            using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                // context.Database.Migrate();

                var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var mike = userMgr.FindByNameAsync("mike").Result;

                if (mike == null)
                {
                    mike = new ApplicationUser
                    {
                        // don't seed identity column - Id = "6de17564-15cd-412f-9804-537b7cbeb4c8",
                        CustomerNumber = 277620619,
                        UserName = "mike",
                        NormalizedUserName = "MIKE",
                        Email = "mike@somemail.com",
                        NormalizedEmail = "MIKE@SOMEMAIL.COM",
                        LockoutEnabled = true
                    };
                    IdentityResult mikeResult = userMgr.CreateAsync(mike, mikePassword).Result;
                    if (!mikeResult.Succeeded) throw new Exception(mikeResult.Errors.First().Description);

                    mikeResult = userMgr.AddClaimsAsync(mike, new Claim[]{
                            new Claim(ClaimTypes.Name, "Mike")
                    }).Result;
                    if (!mikeResult.Succeeded) throw new Exception(mikeResult.Errors.First().Description);

                    var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    if (!await roleMgr.RoleExistsAsync("Customer")) await roleMgr.CreateAsync(new IdentityRole("Customer"));
                    mikeResult = await userMgr.AddToRoleAsync(mike, "Customer");
                    if (!mikeResult.Succeeded) throw new Exception(mikeResult.Errors.First().Description);
                    await userMgr.UpdateAsync(mike);

                    Console.WriteLine("mike user successfully created.");
                }
                else
                {
                    Console.WriteLine("mike already exists.");
                }
            }
        }
    }
}
