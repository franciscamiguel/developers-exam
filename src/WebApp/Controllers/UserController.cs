using Domain.Dtos;
using Domain.Entities;
using Domain.Interfaces.Services;
using FluentValidation;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class UserController(
        ILogger<UserController> logger,
        IUserService userService,
        IEmailService emailService,
        IValidator<UserCreateDto> createValidator,
        IValidator<UserUpdateDto> updateValidator) : Controller
    {

        /// <summary>
        /// Busca todos os usuários
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var users = await userService.GetAllAsync();
            return View(users);
        }

        /// <summary>
        /// Busca um usuário por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Details(long id)
        {
            var user = userService.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        /// <summary>
        /// Exibe a tela de criação de usuário
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Salva usuário
        /// </summary>
        /// <param name="userCreateDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCreateDto userCreateDto)
        {
            var validationResult = await createValidator.ValidateAsync(userCreateDto);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            try
            {
                var existingUserMessage = await CheckUserExistsAsync(userCreateDto);
                if (!string.IsNullOrEmpty(existingUserMessage))
                {
                    ModelState.AddModelError("Create", existingUserMessage);
                    return BadRequest(ModelState);
                }

                var user = InstanceUser(userCreateDto);

                await userService.InsertAsync(user);

                var message = "Olá, você foi cadastrado no sistema Developers Exam";
                emailService.SendEmail(user.Email, message);

                return RedirectToAction("Index");
            }
            catch (ValidationException ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(new { error = ex.Message });
            }
            catch (ArgumentException ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Busca usuário para editar
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Atualiza usuário editado
        /// </summary>
        /// <param name="userUpdateDto"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserUpdateDto userUpdateDto)
        {
            var validationResult = await updateValidator.ValidateAsync(userUpdateDto);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            try
            {
                var user = await userService.GetByIdAsync(userUpdateDto.Id);

                if (user == null)
                    return NotFound();

                user.Udate(userUpdateDto);

                await userService.UpdateAsync(user);

                return RedirectToAction("Index");
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

        /// <summary>
        /// Busca usuários para excluir
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(long id)
        {
            var user = await userService.GetByIdAsync(id);

            if (user == null)
                return NotFound();

            return View(user);
        }

        /// <summary>
        /// Exclui usuários
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            try
            {
                await userService.DeleteAsync(id);

                return RedirectToAction("Index");
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogError(ex.Message, ex);
                return NotFound(ex.Message);
            }
        }

        private async Task<string> CheckUserExistsAsync(UserCreateDto userCreateDto)
        {
            if (await userService.GetByEmailAsync(userCreateDto.Email))
                return "Já Existe usuários com esse email";

            if (await userService.GetByLoginAsync(userCreateDto.Login))
                return "Já Existe usuários com esse login";

            if (await userService.GetUserAsync(userCreateDto.Name, userCreateDto.Surname))
                return "Já Existe usuários com esse nome e sobrenome";

            return null;
        }

        private static User InstanceUser(UserCreateDto userCreateDto)
            =>
            new(
                userCreateDto.Name,
                userCreateDto.Surname,
                userCreateDto.Email,
                userCreateDto.Login,
                userCreateDto.Password,
                userCreateDto.Age
            );


    }
}
