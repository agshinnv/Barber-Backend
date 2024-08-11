using Domain.Models;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AboutService : IAboutService
    {
        private readonly IAboutRepository _aboutRepository;
        public AboutService(IAboutRepository aboutRepository)
        {
            _aboutRepository = aboutRepository;
        }
        public async Task Create(About about)
        {
            await _aboutRepository.Create(about);
        }

        public async Task DeleteAsync(About about)
        {
            await _aboutRepository.Delete(about);
        }

        public async Task DeleteImage(AboutImage image)
        {
            await _aboutRepository.DeleteImage(image);
        }

        public async Task EditAsync(int id, About about)
        {
            var existAbout = await GetByIdAsync(id);

            existAbout.Title = about.Title;
            existAbout.Description = about.Description;
            existAbout.Pro1 = about.Pro1;
            existAbout.Pro2 = about.Pro2;
            existAbout.Pro3 = about.Pro3;
            existAbout.AboutImages = about.AboutImages;

            await _aboutRepository.Edit(existAbout);
        }

        public async Task<About> GetAllAsync()
        {
            var about = await _aboutRepository.GetAllWithImages();
            return about;
            
        }

        public async Task<About> GetByIdAsync(int id)
        {
            return await _aboutRepository.GetByIdWithImages(id);
        }
    }
}
