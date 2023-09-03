using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Imaging;

namespace HomeAPI.Controllers
{
    [Route("homeapi/[controller]")]
    [ApiController]
    public class LoginController : BaseController
    {
        [HttpGet("Code")]
        public IActionResult Code()
        {
            return Ok(new
            {
                code = 0,
                data ="http://"+ Request.HttpContext.Request.Host+ "/homeapi/Login/1",
                message = "获取验证码成功"
            });
        }

        [HttpGet("{id}")]
        public IActionResult GenerateCaptcha()
        {
            int width = 200;
            int height = 80;
            int fontSize = 30;

            // 创建一个位图对象
            using (Bitmap bitmap = new Bitmap(width, height))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    // 设置背景颜色
                    graphics.Clear(Color.White);

                    // 创建随机数生成器
                    Random random = new Random();

                    // 生成验证码字符串
                    string captcha = GenerateRandomString(random, 6);

                    // 将验证码字符串保存到会话或缓存中

                    // 绘制验证码字符串到图像上
                    using (Font font = new Font(FontFamily.GenericSerif, fontSize, FontStyle.Bold))
                    {
                        using (SolidBrush brush = new SolidBrush(Color.Black))
                        {
                            graphics.DrawString(captcha, font, brush, 10, 10);
                        }
                    }

                    // 添加一些干扰
                    for (int i = 0; i < 100; i++)
                    {
                        int x = random.Next(width);
                        int y = random.Next(height);
                        bitmap.SetPixel(x, y, Color.LightGray);
                    }

                    // 将位图保存为 PNG 格式
                    using (MemoryStream stream = new MemoryStream())
                    {
                        bitmap.Save(stream, ImageFormat.Png);
                        stream.Position = 0;

                        // 返回生成的验证码图片
                        return File(stream.ToArray(), "image/png");
                    }
                }
            }
        }

        private string GenerateRandomString(Random random, int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] result = new char[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = chars[random.Next(chars.Length)];
            }

            return new string(result);
        }
    }
}
