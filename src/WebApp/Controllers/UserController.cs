using Domain.Dtos;
using Domain.Entities;
using Domain.Interfaces.Services;
using FluentValidation;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Validation;

namespace WebApp.Controllers
{
    public class UserController(
        ILogger<UserController> logger, 
        IUserService userService,
        IEmailService emailService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var users = await userService.GetAllAsync();
            return View(users);
        }

        public IActionResult Details(long id)
        {
            var user = userService.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCreateDto userCreateDto)
        {
            var validator = new UserCreateDtoValidator();
            var validationResult = await validator.ValidateAsync(userCreateDto);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            try
            {

                var userExists = await userService.GetByEmailAsync(userCreateDto.Email);

                if(userExists)
                {
                    ModelState.AddModelError("", "Já Existe usuários com esse email");
                    return View(userCreateDto);
                }

                var user = new User
                (
                    userCreateDto.Name,
                    userCreateDto.Surname,
                    userCreateDto.Email,
                    userCreateDto.Login,
                    userCreateDto.Password,
                    userCreateDto.Age
                );

                await userService.InsertAsync(user);

                var message = "Olá, você foi cadastrado no sistema Developers Exam";

                emailService.SendEmail(user.Email, message);

                return Redirect("Index");
            }
            catch (ValidationException ex)
            {
                logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> Edit(long id)
        {
            var user = await userService.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            var userUpdateDto = new UserUpdateDto
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Login = user.Login,
                Password = user.Password,
                Email = user.Email,
                Age = user.Age
            };

            return View(userUpdateDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserUpdateDto userUpdateDto)
        {
            var validator = new UserUpdateDtoValidator();
            var validationResult = await validator.ValidateAsync(userUpdateDto);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            try
            {
                var user = await userService.GetByIdAsync(userUpdateDto.Id);

                if (user == null)
                    return NotFound();

                user.Udate(userUpdateDto);

                await userService.UpdateAsync(user);

                return Redirect("Index");
            }
            catch (ValidationException ex)
            {
                logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogError(ex.Message, ex);
                return NotFound(ex.Message);
            }
        }

        public async Task<IActionResult> Delete(long id)
        {
            var user = await userService.GetByIdAsync(id);
         
            if (user == null)
                return NotFound();
            
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            try
            {
                await userService.DeleteAsync(id);

                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogError(ex.Message, ex);
                return NotFound(ex.Message);
            }
        }
    }
}
