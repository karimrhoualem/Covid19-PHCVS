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
                    return "/Users/ahmed/Desktop/Concordia/Courses/Summer_2021/COMP353/Project/username_pwd.txt";
                case User.GABBI:
                    return "/Users/gabrielleguidote/Downloads/comp353_username_pwd.txt";
                case User.KARIM:
                    return "C:/Users/karim/OneDrive/Documents/computer_engineering/summer-2021/COMP-353/projects/project-B/project_B_username_password.txt";
                case User.MJ:
                    return "C:/Users/Caste/OneDrive/Bureau/COMP 353/Assignment/Main_Project/username_password.txt";
                default:
                    return "";
            }
        }
    }
}
