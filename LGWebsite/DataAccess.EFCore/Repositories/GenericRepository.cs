using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Linq.Expressions;
using System.Net;


namespace DataAccess.EFCore.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly LgwebsiteContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(LgwebsiteContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);

        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task RemoveAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
        public (string ImageUrl, string ThumbnailUrl) UploadImage(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentException("No image file provided");
            }
            // Limit the size to 2MB
            if (imageFile.Length > 2 * 1024 * 1024)
            {
                throw new ArgumentException("The image file size must not exceed 2MB.");
            }

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Url/images");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Generate unique file names for the image and thumbnail
            var uniqueFileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(uploadsFolder, uniqueFileName);

            var thumbnailFileName = Guid.NewGuid() + "-thumb" + Path.GetExtension(imageFile.FileName);
            var thumbnailPath = Path.Combine(uploadsFolder, thumbnailFileName);

            // Save the original image
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                imageFile.CopyTo(stream);
            }

            // Create and save the thumbnail
            using (var image = Image.Load(imageFile.OpenReadStream()))
            {
                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Mode = ResizeMode.Max,
                    Size = new Size(150, 150) // Thumbnail size
                }));
                image.Save(thumbnailPath);
            }

            // Return the image and thumbnail URLs
            return ($"/Url/images/{uniqueFileName}", $"/Url/images/{thumbnailFileName}");
        }
        public string UploadIcon(IFormFile iconFile)
        {
            if (iconFile == null || iconFile.Length == 0)
            {
                throw new ArgumentException("No icon file provided");
            }

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/icons");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Validate the file extension
            if (Path.GetExtension(iconFile.FileName).ToLower() != ".svg")
            {
                throw new ArgumentException("Only SVG files are allowed.");
            }

            var uniqueIconName = Guid.NewGuid() + ".svg"; // Ensure .svg extension
            var iconPath = Path.Combine(uploadsFolder, uniqueIconName);

            // Save the icon file
            using (var stream = new FileStream(iconPath, FileMode.Create))
            {
                iconFile.CopyTo(stream);
            }

            // Return the URL of the uploaded icon
            return $"/assets/icons/{uniqueIconName}";
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }


    }
}
