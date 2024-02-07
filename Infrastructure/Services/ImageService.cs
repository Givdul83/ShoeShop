
using Infrastructure.Dtos;
using Infrastructure.ProductEntities;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services;

public class ImageService(ImageRepository imageRepository)
{
      private readonly ImageRepository _imageRepository = imageRepository;

        

    public async Task<Image> CreateImageEntityAsync(ProductRegDto productRegDto)
    {
        try
        {
            var imageEntity = await _imageRepository.GetOneAsync(x => x.ImageUrl == productRegDto.ImageURL);

            if (imageEntity == null)
            {
                var newImageEntity = await _imageRepository.CreateAsync(new Image
                {
                    ImageUrl = productRegDto.ImageURL
                });
                if (newImageEntity != null)
                {
                    return newImageEntity;
                }
                else
                {
                    if (imageEntity != null)
                        return imageEntity;
                }
            }
            return null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR CreateImageEntityAsyc " +ex.Message);
            return null!;
        }
    } 

}
