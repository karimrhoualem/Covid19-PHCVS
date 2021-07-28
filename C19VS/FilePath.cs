using System;

namespace C19VS
{
    public enum User
    {
        AHMED,
        GABBI,
        KARIM,
        MJ
    }

    public static class FilePath
    {
        public static string GetFilePath(User user)
        {
            switch (user)
            {
                case User.AHMED:
                    return "";
                case User.GABBI:
                    return "";
                case User.KARIM:
                    return "C:/Users/karim/OneDrive/Documents/computer_engineering/summer-2021/COMP-353/projects/project-B/project_B_username_password.txt";
                case User.MJ:
                    return "";
                default:
                    return "";
            }
        }
    }
}
