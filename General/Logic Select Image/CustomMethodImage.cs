using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Кастомные методы для работы с Image
/// </summary>
public static class CustomMethodImage
{
    /// <summary>
    /// Отображает любой Sprite (одиночный или из атласа) внутри RawImage
    /// </summary>
    public static void SetSpriteRawImage(RawImage rawImage, Sprite sprite)
    {
        if (rawImage == null) return;

        if (sprite == null)
        {
            rawImage.texture = null;
            return;
        }

        // Назначаем саму текстуру (она может быть целым атласом)
        rawImage.texture = sprite.texture;

        // Рассчитываем правильные UV-координаты конкретного кусочка
        Rect rect = sprite.textureRect;
        float texWidth = sprite.texture.width;
        float texHeight = sprite.texture.height;

        Rect uvRect = new Rect(
            rect.x / texWidth,
            rect.y / texHeight,
            rect.width / texWidth,
            rect.height / texHeight
        );

        // Применяем координаты к RawImage
        rawImage.uvRect = uvRect;
    }
    
    /// <summary>
    /// Создает отдельную Texture2D из любого Sprite (включая атласы) с помощью GPU.
    /// Обязаны удалить её из памяти вручную Destroy(ваша текстура)
    /// </summary>
    public static Texture2D CreateTextureFromSprite(Sprite sprite)
    {
        // Если спрайт одиночный и не обрезан, просто возвращаем его родную текстуру
        if (sprite.rect.width == sprite.texture.width && sprite.rect.height == sprite.texture.height)
        {
            return sprite.texture;
        }

        // Вычисляем размеры и координаты кусочка
        int width = (int)sprite.rect.width;
        int height = (int)sprite.rect.height;

        // Создаем временный буфер на видеокарте (RenderTexture)
        RenderTexture rt = RenderTexture.GetTemporary(width, height, 0, RenderTextureFormat.ARGB32);
        
        RenderTexture.active = rt;
        GL.Clear(false, true, Color.clear);

        //Копируем только нужный квадрат из атласа на GPU
        Graphics.Blit(sprite.texture, rt, 
            new Vector2(sprite.rect.width / sprite.texture.width, sprite.rect.height / sprite.texture.height), 
            new Vector2(sprite.rect.x / sprite.texture.width, sprite.rect.y / sprite.texture.height)
        );

        // Создаем результирующую текстуру и переносим туда данные
        Texture2D result = new Texture2D(width, height, TextureFormat.ARGB32, false);
        result.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        result.Apply();

        // Обязательно наводим за собой порядок в памяти!
        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(rt);

        return result;
    }
}
