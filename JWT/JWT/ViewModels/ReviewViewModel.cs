using JWT.Models;

namespace JWT.ViewModels
{
    public class ReviewViewModel
    {
        public ReviewViewModel()
        {
        }

        public ReviewViewModel(Review review)
        {
            if (review == null)
            {
                return;
            }

            BookId = review.BookId;
            Rating = review.Rating;
            Description = review.Description;
        }

        public int BookId { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }

        public Review ToReview()
        {
            return new Review
            {
                BookId = BookId,
                Description = Description,
                Rating = Rating
            };
        }
    }
}