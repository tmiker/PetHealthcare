using PetHealthcare.API.MapperProfiles;

namespace PetHealthcare.API.CompositionRoot
{
    public static class AutomapperRegistrations
    {
        public static IServiceCollection RegisterAutomapperServices(this IServiceCollection services)
        {

            services.AddAutoMapper(cfg => { }, typeof(PetHealthcareConfig));

            services.AddAutoMapper(cfg => {
                cfg.LicenseKey = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1Y2t5cGVubnlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxNzg4MzkzNjAwIiwiaWF0IjoiMTc1NjkzMjY4NiIsImFjY291bnRfaWQiOiIwMTk5MTE1ODc1OWE3ZDA3OGU3NTE0ZTAxMWI4MzQ4NyIsImN1c3RvbWVyX2lkIjoiY3RtXzAxazQ4bmpnbnJnMjZzdmRnYWtqNjQzcXNlIiwic3ViX2lkIjoiLSIsImVkaXRpb24iOiIwIiwidHlwZSI6IjIifQ.jmdKYTTTsbyPp4zI-pVr4T4jsYwg-M6qJHHBtCCCKXGP_B7duOiEvzFvKkPKNGZ2830BtsJdewfithYQDQawRVVV-MZa1bPa27AiMgMj5H9HDBN84h5XbRszjGhLMxgklMQ1HTFDOBGpJDANE3UwgdM0OJywJfVc2n67lZ-w6_y8W-5_xQGIbBlqVFcYTf3tk5Y2cpE2kPJY0li6BH6W2aBKNSXyvtE8jpxPr5Y2W7gqqHerLWWSPQkNlgm8QGJYXpPJg4EgHvmElM8HUccNfuh-JfCQ2w7hyjkb-MEEIXjaGQ8uCiYaiaGvfQ0sorxrEgUQBQTzS2rdkEn-Gb6TXA";
            });


            return services;
        }
    }
}
