using LGWebsite.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LGWebsite.Api
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IBlogsCategoryService _BlogsCategoryService;
        private readonly IContactService _contactService;
        public ContactController(IBlogsCategoryService BlogsCategoryService, IContactService contactService)
        {
            _BlogsCategoryService = BlogsCategoryService;
            _contactService = contactService;
        }
        [HttpGet("getContactTop")]
        public async Task<IActionResult> GetContactTop()
        {
            var listContact = await _BlogsCategoryService.GetBlogCategoryByPositionAsync(Position.Position_Contact_RightTop);
            foreach (var item in listContact)
            {
                item.ImageUrl = AppSettings.Endpoint + item.ImageUrl;
            }
            return Ok(listContact);
        }

        [HttpGet("getContactBottom")]
        public async Task<IActionResult> GetContactBottom()
        {
            var listContact = await _BlogsCategoryService.GetBlogCategoryByPositionAsync(Position.Position_Contact_RightBottom);
            foreach (var item in listContact)
            {
                item.ImageUrl = AppSettings.Endpoint + item.ImageUrl;
            }
            return Ok(listContact);
        }




        [HttpPost("postContact")]
        public async Task<IActionResult> PostContact([FromBody] Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _contactService.AddContactAsync(contact);

                if (result)
                {
                    return CreatedAtAction(nameof(GetContactTop), new { id = contact.Id }, contact);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new contact");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred");
            }
        }


    }
}
