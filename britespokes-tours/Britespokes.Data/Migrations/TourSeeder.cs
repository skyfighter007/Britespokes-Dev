using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Diagnostics;
using Britespokes.Core.Domain;

namespace Britespokes.Data.Migrations
{
  internal class TourSeeder
  {
    public static void Seed(EfDbContext context, List<Organization> organizations)
    {
      foreach (var tour in CoverYourBasesTours(organizations))
        context.Tours.AddOrUpdate(t => t.Code, tour);
      foreach (var tour in CrunchFitnessTours(organizations))
        context.Tours.AddOrUpdate(t => t.Code, tour);
      foreach (var tour in TheLegendsContinueTours(organizations))
        context.Tours.AddOrUpdate(t => t.Code, tour);
      foreach (var tour in LetsRallyTours(organizations))
        context.Tours.AddOrUpdate(t => t.Code, tour);
      foreach (var tour in GetTheDriftTours(organizations))
        context.Tours.AddOrUpdate(t => t.Code, tour);
      foreach (var tour in GetSuperBowlTours(organizations))
        context.Tours.AddOrUpdate(t => t.Code, tour);
      foreach (var tour in GetImbibeRideTours(organizations))
        context.Tours.AddOrUpdate(t => t.Code, tour);

      try
      {
        context.SaveChanges();
      }
      catch (DbEntityValidationException dbex)
      {
        foreach (var dbEntityValidationResult in dbex.EntityValidationErrors)
          foreach (var dbValidationError in dbEntityValidationResult.ValidationErrors)
            Debug.WriteLine("{0}: {1}", dbValidationError.PropertyName, dbValidationError.ErrorMessage);
        throw;
      }
    }

    private static IEnumerable<Tour> CoverYourBasesTours(List<Organization> organizations)
    {
      var tour = new Tour
        {
          Code = "CYB4NY",
          Name = "Cover Your Bases",
          Permalink = "cover-your-bases-nyc",
          IsPublished = true,
          LengthDescription = "4 day/3 night",
          DepartureCity = "New York, NY",
          ReturningCity = "New York, NY",
          AvailabilityStatusId = 3
        };

      var departures = new List<Departure>
        {
          new Departure
            {
              Code = "CYB4NY01",
              DepartureDate = new DateTime(2013, 8, 23),
              ReturnDate = new DateTime(2013, 8, 26),
              Product = CreateProduct(tour.Name, organizations, 1399.0M, 1099.0M, 899.0M, 799.0M),
              AvailabilityStatusId = 1
            }
        };

      tour.Departures = departures;
      return new[] { tour };
    }

    private static IEnumerable<Tour> CrunchFitnessTours(List<Organization> organizations)
    {
      var tours = new List<Tour>
        {
          new Tour
            {
              Code = "HAF4NY",
              Name = "Love.Fitness.Happiness with Crunch Fitness",
              Permalink = "crunch-fitness-health-road-trips-nyc",
              IsPublished = true,
              LengthDescription = "3 day/2 night",
              DepartureCity = "New York, NY",
              ReturningCity = "New York, NY",
              AvailabilityStatusId = 2,
              Departures = new List<Departure>
                {
                  new Departure
                    {
                      Code = "HAF4NY01",
                      DepartureDate = new DateTime(2013, 9, 27),
                      ReturnDate = new DateTime(2013, 9, 29),
                      Product = CreateProduct("Love.Fitness.Happiness with Crunch Fitness", organizations, 3699.0M, 2999.0M, 2899.0M, 2699.0M),
                      AvailabilityStatusId = 2
                    },
                  new Departure
                    {
                      Code = "HAF4NY02",
                      DepartureDate = new DateTime(2013, 10, 25),
                      ReturnDate = new DateTime(2013, 10, 27),
                      Product = CreateProduct("Love.Fitness.Happiness with Crunch Fitness", organizations, 3699.0M, 2999.0M, 2899.0M, 2699.0M),
                      AvailabilityStatusId = 2
                    }
                }
            },
          new Tour
            {
              Code = "HAF7SW",
              Name = "Crunch Fitness & Health Road Trips",
              Permalink = "crunch-fitness-health-road-trips-scottsdale",
              IsPublished = true,
              LengthDescription = "7 day/6 night",
              DepartureCity = "Scottsdale, AZ",
              ReturningCity = "Las Vegas, NV",
              AvailabilityStatusId = 1,
              Departures = new List<Departure>
                {
                  new Departure
                    {
                      Code = "HAF7SW01",
                      DepartureDate = new DateTime(2013, 9, 29),
                      ReturnDate = new DateTime(2013, 10, 5),
                      Product = CreateProduct("Crunch Fitness & Health Road Trips", organizations, 5199.0M, 4199.0M, 3999.0M, 3799.0M),
                      AvailabilityStatusId = 1
                    }
                }
            }
        };

      return tours;
    }

    private static IEnumerable<Tour> TheLegendsContinueTours(List<Organization> organizations)
    {
      var tours = new List<Tour>
        {
          new Tour
            {
              Code = "HOF3CH",
              Name = "The Legends Continue",
              Permalink = "the-legends-continue-chicago",
              IsPublished = true,
              LengthDescription = "3 day/2 night",
              DepartureCity = "Chicago, IL",
              ReturningCity = "Chicago, IL",
              AvailabilityStatusId = 1,
              Departures = new List<Departure>
                {
                  new Departure
                    {
                      Code = "HOF3CH02",
                      DepartureDate = new DateTime(2013, 10, 25),
                      ReturnDate = new DateTime(2013, 10, 27),
                      Product = CreateProduct("The Legends Continue", organizations, 1599.0M, 1399.0M,	1299.0M, 1199.0M),
                      AvailabilityStatusId = 1
                    },
                  new Departure
                    {
                      Code = "HOF3CH01",
                      DepartureDate = new DateTime(2013, 11, 8),
                      ReturnDate = new DateTime(2013, 11, 10),
                      Product = CreateProduct("The Legends Continue", organizations, 1599.0M, 1399.0M,	1299.0M, 1199.0M),
                      AvailabilityStatusId = 1
                    }
                }
            },
          new Tour
            {
              Code = "HOF3NY",
              Name = "The Legends Continue",
              Permalink = "the-legends-continue-nyc",
              IsPublished = true,
              LengthDescription = "3 day/2 night",
              DepartureCity = "New York, NY",
              ReturningCity = "New York, NY",
              AvailabilityStatusId = 1,
              Departures = new List<Departure>
                {
                  new Departure
                    {
                      Code = "HOF3NY02",
                      DepartureDate = new DateTime(2013, 10, 25),
                      ReturnDate = new DateTime(2013, 10, 27),
                      Product = CreateProduct("The Legends Continue", organizations, 1599.0M, 1399.0M,	1299.0M, 1199.0M),
                      AvailabilityStatusId = 1
                    },
                  new Departure
                    {
                      Code = "HOF3NY01",
                      DepartureDate = new DateTime(2013, 11, 8),
                      ReturnDate = new DateTime(2013, 11, 10),
                      Product = CreateProduct("The Legends Continue", organizations, 1599.0M, 1399.0M,	1299.0M, 1199.0M),
                      AvailabilityStatusId = 1
                    }
                }
            },
          new Tour
            {
              Code = "HOF7CH",
              Name = "The Legends Continue",
              Permalink = "the-legends-continue-chicago-nyc",
              IsPublished = true,
              LengthDescription = "6 day/5 night",
              DepartureCity = "Chicago, IL",
              ReturningCity = "New York, NY",
              AvailabilityStatusId = 1,
              Departures = new List<Departure>
                {
                  new Departure
                    {
                      Code = "HOF7CH01",
                      DepartureDate = new DateTime(2013, 10, 12),
                      ReturnDate = new DateTime(2013, 10, 17),
                      Product = CreateProduct("The Legends Continue", organizations, 3999.0M, 3299.0M,	2899.0M, 2799.0M),
                      AvailabilityStatusId = 1
                    }
                }
            },
          new Tour
            {
              Code = "HOF7NY",
              Name = "The Legends Continue",
              Permalink = "the-legends-continue-nyc-chicago",
              IsPublished = true,
              LengthDescription = "6 day/6 night",
              DepartureCity = "New York, NY",
              ReturningCity = "Chicago, IL",
              AvailabilityStatusId = 1,
              Departures = new List<Departure>
                {
                  new Departure
                    {
                      Code = "HOF7NY01",
                      DepartureDate = new DateTime(2013, 9, 16),
                      ReturnDate = new DateTime(2013, 9, 22),
                      Product = CreateProduct("The Legends Continue", organizations, 3999.0M, 3299.0M,	2899.0M, 2799.0M),
                      AvailabilityStatusId = 1
                    }
                }
            },
        };

      return tours;
    }

    private static IEnumerable<Tour> LetsRallyTours(List<Organization> organizations)
    {
      var tours = new List<Tour>
        {
          new Tour
            {
              Code = "MSR4NY",
              Name = "Let's Rally",
              Permalink = "lets-rally-nyc",
              IsPublished = true,
              LengthDescription = "2 day/1 night",
              DepartureCity = "New York, NY",
              ReturningCity = "New York, NY",
              AvailabilityStatusId = 1,
              Departures = new List<Departure>
                {
                  new Departure
                    {
                      Code = "MSR4NY01",
                      DepartureDate = new DateTime(2013, 9, 7),
                      ReturnDate = new DateTime(2013, 9, 8),
                      Product = CreateProduct("Let's Rally", organizations, 6199.0M, 5799.0M,	5699.0M, 5599.0M),
                      AvailabilityStatusId = 3
                    },
                  new Departure
                    {
                      Code = "MSR2NY02",
                      DepartureDate = new DateTime(2013, 9, 14),
                      ReturnDate = new DateTime(2013, 9, 15),
                      Product = CreateProduct("Let's Rally", organizations, 6199.0M, 5799.0M,	5699.0M, 5599.0M),
                      AvailabilityStatusId = 1
                    }
                  //new Departure
                  //  {
                  //    Code = "MSR4NY03",
                  //    DepartureNumber = 3,
                  //    DepartureDate = new DateTime(2013, 10, 11),
                  //    ReturnDate = new DateTime(2013, 10, 14),
                  //    Product = CreateProduct("Let's Rally", organizations, 8699.0M, 8399.0M,	8199.0M, 7999.0M)
                  //  }
                }
            }
        };

      return tours;
    }

    private static IEnumerable<Tour> GetTheDriftTours(List<Organization> organizations)
    {
      var tours = new List<Tour>
        {
          new Tour
            {
              Code = "MSD3LA",
              Name = "Get the Drift",
              Permalink = "get-the-drift-la",
              IsPublished = true,
              LengthDescription = "3 day/2 night",
              DepartureCity = "Los Angeles, CA",
              ReturningCity = "Los Angeles, CA",
              AvailabilityStatusId = 1,
              Departures = new List<Departure>
                {
                  new Departure
                    {
                      Code = "MSD3LA01",
                      DepartureDate = new DateTime(2013, 11, 15),
                      ReturnDate = new DateTime(2013, 11, 17),
                      Product = CreateProduct("Get the Drift", organizations, 6499.0M, 6099.0M,	5799.0M, 5699.0M),
                      AvailabilityStatusId = 1
                    }
                }
            }
        };

      return tours;
    }

    private static IEnumerable<Tour> GetSuperBowlTours(List<Organization> organizations)
    {
      var tours = new List<Tour>
        {
          new Tour
            {
              Code = "SB1NYC",
              Name = "Super Bowl XLVIII",
              Permalink = "super-bowl-xlviii-nyc",
              IsPublished = true,
              LengthDescription = "3 day/2 night",
              DepartureCity = "New York, NY",
              ReturningCity = "New York, NY",
              AvailabilityStatusId = 2,
              MailingListUrl = "http://email.britespokes.com/t/t/s/jltttl/",
              MailingListEmailName = "cm-jltttl-jltttl",
              Departures = new List<Departure>
                {
                  new Departure
                    {
                      Code = "SB1NYC01",
                      DepartureDate = new DateTime(2014, 1, 31),
                      ReturnDate = new DateTime(2014, 2, 2),
                      Product = CreateProduct("Super Bowl XLVIII", organizations, 0.0M, 0.0M, 0.0M, 0.0M),
                      AvailabilityStatusId = 2
                    }
                }
            }
        };

      return tours;
    }

    private static IEnumerable<Tour> GetImbibeRideTours(List<Organization> organizations)
    {
      var tours = new List<Tour>
        {
          new Tour
            {
              Code = "MX03PH",
              Name = "Imbibe Ride",
              Permalink = "imbibe-ride-philadelphia",
              IsPublished = true,
              LengthDescription = "3 day / 2 night",
              DepartureCity = "Philadelphia, PA",
              ReturningCity = "Philadelphia, PA",
              AvailabilityStatusId = 2,
              MailingListUrl = "http://email.britespokes.com/t/t/s/jlttjk/",
              MailingListEmailName = "cm-jlttjk-jlttjk",
              Departures = new List<Departure>
                {
                  new Departure
                    {
                      Code = "MX03PH01",
                      DepartureDate = new DateTime(2013, 9, 16),
                      ReturnDate = new DateTime(2013, 9, 22),
                      Product = CreateProduct("Imbibe Ride", organizations, 0.0M, 0.0M, 0.0M, 0.0M),
                      AvailabilityStatusId = 2
                    }
                }
            }
        };

      return tours;
    }

    private static Product CreateProduct(string name, List<Organization> organizations,
      decimal singlePrice, decimal doublePrice,
      decimal triplePrice, decimal quadPrice)
    {
      var product = new Product
        {
          Name = name,
          Organizations = organizations,
          ProductVariants = new List<ProductVariant>
            {
              new ProductVariant
                {
                  Name = "Single",
                  DisplayName = "Single",
                  PluralDisplayName = "Singles",
                  Description = "1 person in the room",
                  Price = singlePrice
                },
              new ProductVariant
                {
                  Name = "Double",
                  DisplayName = "Double",
                  PluralDisplayName = "Doubles",
                  Description = "2 people in the room",
                  Price = doublePrice,
                  IsMaster = true
                },
              new ProductVariant
                {
                  Name = "Triple",
                  DisplayName = "Triple",
                  PluralDisplayName = "Triples",
                  Description = "3 people in the room",
                  Price = triplePrice
                },
              new ProductVariant
                {
                  Name = "Quad",
                  DisplayName = "Quad",
                  PluralDisplayName = "Quads",
                  Description = "4 people in the room",
                  Price = quadPrice
                },
            }
        };

      return product;
    }
  }
}