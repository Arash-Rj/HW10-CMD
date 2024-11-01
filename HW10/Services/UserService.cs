using HW10.Entity;
using HW10.Enum;
using HW10.Interface;
using HW10.Repository;
using Newtonsoft.Json;

namespace HW10.Services
{
    public class UserService : IUserService
    {
        DapperRepo _userRepo = new DapperRepo();
        public Result ChangePass(string newpass, string oldpass)
        {
            bool check = _userRepo._OnlineUser.changepassword(newpass, oldpass);
            if (check == true)
            {
                _userRepo.Update(_userRepo._OnlineUser);
                return new Result(true, "PassWord changed successfully");
            }
            return new Result(false, "The old password is incorrect. Please try again.");
        }

        public Result ChangeStat(string newstatus)
        {
            if (StatusEnum.Available.ToString() == newstatus)
            {
                _userRepo._OnlineUser.Status = StatusEnum.Available;
                _userRepo.Update(_userRepo._OnlineUser);
                return new Result(true, "Status changed successfully.");
            }
            else
            {
                _userRepo._OnlineUser.Status = StatusEnum.UnAvailable;
                _userRepo.Update(_userRepo._OnlineUser);
                return new Result(true, "Status changed successfully.");
            }
            return null;
        }

        public Result Logout()
        {
                    string data = null;
                    _userRepo._OnlineUser = null;
                    return new Result(true, "Logout successful.");
        }

        public Result Login(string username, string password)
        {
            var users = _userRepo.GetAll();
            try
            {
                foreach (var user in users)
                {
                    if (user.UserName == username && user.CheckPass(password) == true)
                    {
                        _userRepo._OnlineUser = user;
                        return new Result(true, "Login was successful.");
                    }
                }
                return new Result(false, "Error: User was not found! Try again.");
            }
            catch (NullReferenceException)
            {
                return new Result(false, "Error: User was not found! Try again.");
            }
        }

        public Result Register(string username, string password)
        {
            User newuser = new User(username, password);
            List<User> users = _userRepo.GetAll();
            if (users is null)
            {
                _userRepo.Add(newuser);
            }
            else
            {
                foreach (var user in users)
                {
                    if (user.UserName.Equals(username))
                    {
                        return new Result(false, "Register failed. User already exists");
                    }
                    if (user.CheckPass(password) == true)
                    {
                        return new Result(false, "Register failed,The password is already used.");
                    }
                }
                _userRepo.Add(newuser);
            }
            return new Result(true, "Register is done successfully.");
        }

        public List<User> Search(string username)
        {
            var users = _userRepo.GetAll(); 
            List<User> result = new List<User>();
            result = users.Where(u => u.UserName.StartsWith(username)).ToList();
            return result;
        }
        public string CheckCommand(string command, out List<User> result)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            result = null;
            if (command is "")
            {
                return "";
            }
            if (command == CommandEnum.cls.ToString())
            {
                return command;
            }
            string[] com = command.Split(" ");
            string username = null;
            string password = null;
            switch (com[0].ToLower())
            {
                case "register":
                    if (_userRepo._OnlineUser is not null)
                    {
                        return $"Security Error: You can not register in the system." +
                            $"User {_userRepo._OnlineUser.UserName} is already logged in the system.";
                    }
                    username = GetUserName(command, out bool check5);
                    password = GetPass(command, out bool check6);
                    if (check6 == false || check5 == false)
                    {
                        return "FormatExceptionError: The request must be in Correct Format.use command *--help*.";
                    }
                    if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                    {
                        Result res1 = Register(username, password);
                        return res1._Messege;
                    }
                    else
                    {
                        return "False request Error: Both --username and --password must be provided.";
                    }
                case "login":
                    if (_userRepo._OnlineUser is not null)
                    {
                        return $"Security Error: You can not log in the system." +
                            $"User {_userRepo._OnlineUser.UserName} is already logged in the system.";
                    }
                    username = GetUserName(command, out bool check);
                    password = GetPass(command, out bool check2);
                    if (check == false || check2 == false)
                    {
                        return "FormatExceptionError: The request must be in Correct Format.use command *--help*.";
                    }
                    if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                    {
                        Result res2 = Login(username, password);
                        return res2._Messege;
                    }
                    if (username == null || password == null)
                    {
                        return "False request Error: Both --username and --password must be provided.";
                    }
                    break;
                case "changepassword":
                    if (_userRepo._OnlineUser is null)
                    {
                        return "Security Error: You are not logged in the system. Please login and then try.";
                    }
                    else if (_userRepo._OnlineUser.Status == StatusEnum.UnAvailable)
                    {
                        return "Error: The user is not available right now. Please change status first.";
                    }
                    password = GetPass(command, out bool check3);
                    string newpass = null;
                    for (int i = 1; i < com.Length; i++)
                    {
                        if (com[i].StartsWith("--new"))
                        {
                            newpass = com[i + 1];
                            break;
                        }
                        newpass = "Wrong";
                    }
                    if (newpass == "Wrong" || check3 == false)
                    {
                        return "FormatExceptionError: The request must be in Correct Format. use command *--help*.";
                    }
                    if (!string.IsNullOrEmpty(newpass) && !string.IsNullOrEmpty(password))
                    {
                        Result res3 = ChangePass(newpass, password);
                        return res3._Messege;
                    }
                    else if (password == null || newpass == null)
                    {
                        return "False request Error: Both --old and --new must be provided.";
                    }
                    break;
                case "change":
                    if (_userRepo._OnlineUser is null)
                    {
                        return "Security Error: You are not logged in the system. Please login and then try.";
                    }
                    string? newstat;
                    for (int i = 1; i < com.Length; i++)
                    {
                        if (com[i].StartsWith("--status"))
                        {
                            if (com[i + 1] == StatusEnum.Available.ToString() ||
                                com[i + 1] == StatusEnum.UnAvailable.ToString())
                            {
                                newstat = com[i + 1];
                                Result res4 = ChangeStat(newstat);
                                return res4._Messege;
                            }
                            else
                            {
                                return "False request Error: The new Status must be provided Correctly.Please try again.";
                            }
                        }
                        return "FormatExceptionError: The request must be in Correct Format.use command *--help*.";
                    }
                    break;
                case "search":
                    if (_userRepo._OnlineUser is null)
                    {
                        return "Security Error: You are not logged in the system. Please login and then try.";
                    }
                    username = GetUserName(command, out bool check4);
                    if (check4 == false)
                    {
                        return "FormatExceptionError: The request must be in Correct Format.use command *--help*.";
                    }
                    if (username == null)
                    {
                        return "False request Error: The username must be Provided. Try again.";
                    }
                    else if (username is not null)
                    {
                        result = Search(username);
                        return "Result: ";
                    }
                    break;
                case "logout":
                    if (_userRepo._OnlineUser is null)
                    {
                        return "Error: No user is logged in the system!";
                    }
                    Result res = Logout();
                    return res._Messege;
                case "--help":
                    return CommandEnum.help.ToString();
                default:
                    return $"Command Error: {com[0]} is not a command. use command *--help*.";
            }
            return null;
        }
        public string GetUserName(string command, out bool check)
        {
            check = false;
            string[] com = command.Split(" ");
            string username = null;
            for (int i = 1; i < com.Length; i++)
            {
                if (com[i].StartsWith("--username"))
                {
                    username = com[i + 1];
                    check = true;
                    return username;
                }
            }
            return null;
        }
        public string GetPass(string command, out bool check)
        {
            check = false;
            string[] com = command.Split(" ");
            string password = null;
            for (int i = 1; i < com.Length; i++)
            {
                if (com[i].StartsWith("--password") || com[i].StartsWith("--old"))
                {
                    password = com[i + 1];
                    check = true;
                    return password;
                }
            }
            return null;
        }
    }
}
