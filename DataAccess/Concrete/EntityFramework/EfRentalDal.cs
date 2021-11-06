using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, CarRentalContext>, IRentalDal
    {
        public List<RentalDetailDto> GetCarRentalDetails(Expression<Func<Rental, bool>> filter = null)
        {
            using (CarRentalContext context = new CarRentalContext())
            {
                var result = from user in context.Users
                             join customer in context.Customers
                             on user.Id equals customer.UserId
                             join rental in context.Rentals
                             on customer.Id equals rental.CustomerId
                             join car in context.Cars
                             on rental.CarId equals car.Id
                             join brand in context.Brands
                             on car.BrandId equals brand.Id
                             join color in context.Colors
                             on car.ColorId equals color.Id
                             select new RentalDetailDto
                             {
                                 BrandName = brand.Name,
                                 ColorName = color.Name,
                                 Id = rental.Id,
                                 FirstName = user.FirstName,
                                 LastName = user.LastName,
                                 DailyPrice = car.DailyPrice,
                                 ModelYear = car.ModelYear,
                                 RentDate = rental.RentDate,
                                 ReturnDate = rental.ReturnDate
                             };
                return result.ToList();
            }
        }
    }
}
