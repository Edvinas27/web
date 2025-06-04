using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Extensions.Mappers
{
    public static class ImageMapper
    {

        public static ImagesDto ToDto(this Images image)
        {
            return new ImagesDto
            {
                Context = image.Context,
                Url = image.Url
            };
        }

        public static List<ImagesDto> ToDtoList(this IEnumerable<Images> images)
        {
            return images.Select(image => image.ToDto()).ToList();
        }
    }
}