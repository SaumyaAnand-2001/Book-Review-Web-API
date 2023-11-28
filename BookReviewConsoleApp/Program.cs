using BookReview.Dto;
using BookReview.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;

namespace Program
{
    class BookReviewConsoleApp
    { 
        static HttpClient client= new HttpClient();
        static string baseUrl = "https://localhost:7046/api/";
        static void Main(string[] args)
        {
            Console.WriteLine(
                "List of Operations:\n" +
                "1. Press 1 to add a book\n" +
                "2. Press 2 to update a book\n" +
                "3.Press 3 to view the list of books\n" +
                "4.Press 4 to get book by id\n" + 
                "5. Press 5 to delete the book\n" +
                "6.Press 6 to write a review\n" +
                "7. Press 7 to update the Book\n" +
                "8. Press 8 to delete the review\n" +
                "9.Press to exit"
                );
            bool flag = true;
            while ( flag )
            {
                Console.WriteLine("Enter Your Choice");
                int input=int.Parse(Console.ReadLine());
                if (input > 0 && input < 9)
                {
                    switch (input)
                    {
                        case 1:
                            addBook().Wait();
                            break;
                        case 2:
                            updateBook().Wait(); break;
                        case 3:
                            allBooks().Wait(); break;   
                        case 4:
                            bookById().Wait(); break;
                        case 5:
                            deleteBook().Wait(); break;
                        case 6:
                            addReview().Wait(); break;
                        case 7:
                            updateReview().Wait(); break;
                        case 8:
                            deleteReview() .Wait(); break;
                    }
                }
                else if (input == 0)
                {
                    flag = false;
                }
                else
                { 
                    Console.WriteLine("Invalid Input\nPlease enter again.");
                    Console.WriteLine(
                    "List of Operations:\n" +
                    "1. Press 1 to add a book\n" +
                    "2. Press 2 to update a book\n" +
                    "3.Press 3 to view the list of books\n" +
                    "4.Press 4 to get book by id\n" +
                    "5. Press 5 to delete the book\n" +
                    "6.Press 6 to write a review\n" +
                    "7. Press 7 to update the Book\n" +
                    "8. Press 8 to delete the review\n" +
                    "9.Press to exit"
                    );
                }
            }

        }

        static async Task allBooks() {
            try 
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri= new Uri(baseUrl +"Book/allbook")
                };
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    List<BookDto> books = await response.Content.ReadAsAsync<List<BookDto>>();
                    foreach (BookDto book in books)
                    {
                        Console.WriteLine(book);
                    }
                }
                else {
                    Console.WriteLine("Error Occurred");
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);                    
            }
        }
        static async Task addBook()
        {
            Console.WriteLine("Enter the book name:");
            string name=Console.ReadLine();
            Console.WriteLine("Ënter the author name");
            string authName = Console.ReadLine();
            BookRequestDto book = new BookRequestDto();
            book.Name = name;
            book.AuthName = authName;
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(baseUrl + "Book/addbook"),
                    Content= new StringContent(JsonConvert.SerializeObject(book),Encoding.UTF8,"application/json")
                };
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Book Added");
                }
                else
                {
                    Console.WriteLine("Error Occurred");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static async Task updateBook()
        {
            
            BookRequestDto book = new BookRequestDto();
            Console.WriteLine("Enter the book id:");
            book.Id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the book name:");
            book.Name = Console.ReadLine();
            Console.WriteLine("Ënter the author name");
            book.AuthName = Console.ReadLine();
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri(baseUrl + "Book/updatebook"),
                    Content = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json")
                };
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Book Updated");
                }
                else
                {
                    Console.WriteLine("Error Occurred");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static async Task deleteBook()
        {
            BookRequestDto book = new BookRequestDto();
            Console.WriteLine("Enter the book id:");
            book.Id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the book name:");
            book.Name = Console.ReadLine();
            Console.WriteLine("Enter the author name");
            book.AuthName = Console.ReadLine();
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri(baseUrl + "Book/deletebook"),
                    Content = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json")
                };
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Book Deleted");
                }
                else
                {
                    Console.WriteLine(response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static async Task bookById()
        {
            Console.WriteLine("Enter the book id:");
            int id = int.Parse(Console.ReadLine());
            string url = "Book/book/"+id;
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(baseUrl + url),
                };
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    BookDto book= await response.Content.ReadAsAsync<BookDto>();
                    Console.WriteLine(book);
                }
                else
                {
                    Console.WriteLine("Error Occurred");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static async Task addReview()
        {
            Console.WriteLine("Enter the review:");
            string comment= Console.ReadLine();
            Console.WriteLine("Enter the book id:");
            int id = int.Parse(Console.ReadLine());
            ReviewDto review= new ReviewDto();
            review.comment = comment;
            review.bookId = id;
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(baseUrl + "Review/review"),
                    Content = new StringContent(JsonConvert.SerializeObject(review), Encoding.UTF8, "application/json")
                };
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Review Added");
                }
                else
                {
                    Console.WriteLine("Error Occurred");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static async Task updateReview()
        {
            ReviewDto review = new ReviewDto();
            Console.WriteLine("Enter the review id:");
            review.Id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the updated comment:");
            review.comment = Console.ReadLine();
            Console.WriteLine("Enter the book id:");
            review.bookId = int.Parse(Console.ReadLine());

            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri(baseUrl + "Review/updatereview"),
                    Content = new StringContent(JsonConvert.SerializeObject(review), Encoding.UTF8, "application/json")
                };
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Review Updated");
                }
                else
                {
                    Console.WriteLine("Error Occurred");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static async Task deleteReview()
        {
            ReviewDto review = new ReviewDto();
            Console.WriteLine("Enter the review id:");
            review.Id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the comment:");
            review.comment = Console.ReadLine();
            Console.WriteLine("Enter the book id:");
            review.bookId = int.Parse(Console.ReadLine());
           
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri(baseUrl + "Review/deletereview"),
                    Content = new StringContent(JsonConvert.SerializeObject(review), Encoding.UTF8, "application/json")
                };
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Review Deleted");
                }
                else
                {
                    Console.WriteLine("Error Occurred");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}