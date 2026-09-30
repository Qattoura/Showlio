using FluentValidation;
using Showlio.api.Interfaces.IRepositories;
using Showlio.api.Interfaces.IService;
using Showlio.api.Interfaces.IServices;
using Showlio.api.Repositories;
using Showlio.api.Services;
using Showlio.api.validators.Certificate;
using Showlio.api.validators.ContactItem;
using Showlio.api.validators.Experience;
using Showlio.api.validators.Portfolio;
using Showlio.api.validators.Project;
using Showlio.api.validators.Service;
using Showlio.api.validators.Skill;
using Showlio.api.validators.Template;
using Showlio.api.Validators.Education;

namespace Showlio.api
{
    public static class ServicesRegistration
    {
        private static void AddCoreServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
        }

        private static void AddPortfolioServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();
            builder.Services.AddScoped<IPortfolioService, PortfolioService>();
            builder.Services.AddScoped<IPortfolioAuthorizationService, PortfolioAuthorizationService>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreatePortfolioDtoValidator>();
        }

        private static void AddSkillServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ISkillRepository, SkillRepository>();
            builder.Services.AddScoped<ISkillService, SkillService>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateSkillDtoValidator>();
        }

        private static void AddProjectServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
            builder.Services.AddScoped<IProjectService, ProjectService>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateProjectDtoValidator>();
        }

        private static void AddExperienceServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IExperienceRepository, ExperienceRepository>();
            builder.Services.AddScoped<IExperienceService, ExperienceService>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateExperienceDtoValidator>();
        }

        private static void AddCertificateServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICertificateRepository, CertificateRepository>();
            builder.Services.AddScoped<ICertificateService, CertificateService>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateCertificateDtoValidator>();
        }
        private static void AddEducationServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IEducationRepository, EducationRepository>();
            builder.Services.AddScoped<IEducationService, EducationService>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateEducationDtoValidator>();
        }

        private static void AddServiceServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
            builder.Services.AddScoped<IServiceService, ServiceService>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateServiceDtoValidator>();
        }

        private static void AddContactItemServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IContactItemRepository, ContactItemRepository>();
            builder.Services.AddScoped<IContactItemService, ContactItemService>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateContactItemDtoValidator>();
        }

        private static void AddTemplateServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ITemplateRepository, TemplateRepository>();
            builder.Services.AddScoped<ITemplateService, TemplateService>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateTemplateValidator>();
        }

        private static void AddPublicPortfolioServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IPublicPortfolioService, PublicPortfolioService>();
        }


        public static void AddServiceRegistrations(this WebApplicationBuilder builder)
        {
            builder.AddCoreServices();

            builder.AddPortfolioServices();
            builder.AddSkillServices();
            builder.AddProjectServices();
            builder.AddExperienceServices();
            builder.AddCertificateServices();
            builder.AddEducationServices();
            builder.AddServiceServices();
            builder.AddContactItemServices();
            builder.AddTemplateServices();
            builder.AddPublicPortfolioServices();

        }
    }
}
