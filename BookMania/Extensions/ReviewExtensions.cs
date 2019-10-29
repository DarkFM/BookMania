using BookMania.Data.Models;
using BookMania.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.Extensions
{
    public static class ReviewExtensions
    {
        public static ReviewModel ToReviewModel(this Review review)
        {
            return new ReviewModel
            {
                UserName = review.User.UserName,
                Rating = review.Rating,
                ReviewText = review.ReviewText
            };
        }
    }
}
