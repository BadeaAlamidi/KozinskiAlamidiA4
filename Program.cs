/* Assignment 2
 * CSCI 473 / 504
 * 
 * September 23, 2021
 * 
 * Badea Alamidi, Z1882808
 * Katelyn Kozinski, Z167824
 * 
 * This program emulates some of the functionality of Reddit, using a graphical interface.
 * 
 * Users can log in, view posts and comments from the various subreddits, and
 * add posts and comments. Users can also delete their own posts and comments
 * (and administrators can delete others' posts and comments).
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KozinskiAlamidiAssignment2
{
    /* Class name: User
    * 
    * This class contains information about Reddit users.
    * 
    * Attributes:
    * 
    * readonly uint id             User ID
    * private readonly UserType    enum type to denote user Type
    * readonly string name         User name
    *  readonly string passwordHash      string with hexidecimal representation used to authenticate log-in's
    * int postScore                Popularity of posts
    * int commentScore             Popularity of comments
    * string[] moderatingSubs      string array with subreddits a user has extra locking authorities on
    * 
    * Properties:
    * 
    * Id                           Getter
    * Type                         Getter
    * Name                         Getter
    * passwordHash                 Getter
    * PostScore                    Getter & setter
    * CommentScore                 Getter & setter
    * TotalScore                   Getter for PostScore + CommentScore
    * moderatingSubs               Getter & setter
    * 
    * Constructors:
    * 
    * User()                       Default
    * User(string[])               For reading from file
    * User(string)                 For user entered data
    * 
    * Restrictions:
    * 
    * User IDs must be unique
    * User names must be between 5 and 21 characters
    * User names must not begin or end with space character
    * 
    * Other notes:
    * 
    * Sortable by Name using the IComparable interface
    * Overrides ToString()
    * this class contains its own enum type used to dictate whether
    * a user is a normal user, a moderator, or an administrator
    */
    public class User : IComparable
    {
        // Attributes
        private readonly uint id;
        private readonly UserType userType;
        private readonly string name;
        private readonly string passwordHash;
        private int postScore;
        private int commentScore;
        private string[] moderatingSubs;

        // Properties to control read/write access to private attributes
        public uint Id => id;
        public UserType Type => userType;
        public string Name => name;
        public string PasswordHash => passwordHash;
        public int PostScore
        {
            get { return postScore; }
            set { postScore = value; }
        }
        public int CommentScore
        {
            get { return commentScore; }
            set { commentScore = value; }
        }
        public int TotalScore => PostScore + CommentScore;
        public string[] ModeratingSubs
        {
            get { return moderatingSubs; }
            set { moderatingSubs = value; } // Value should be an array of strings
        }

        // Default constructor
        public User()
        {
            id = 0;
            userType = UserType.User;
            name = "";
            passwordHash = "";
            PostScore = 0;
            CommentScore = 0;
            ModeratingSubs = new string[0];
        }

        // Alternate constructor (for reading from a file)
        public User(string[] userData)
        {
            string newName = userData[2];

            // Rejects input name if it begins or ends with space characters
            if (Char.IsWhiteSpace(newName, 0) || Char.IsWhiteSpace(newName, newName.Length - 1))
                throw new ArgumentException("Error: Could not add user \"" + newName + "\" from file; usernames cannot begin or end with space characters");

            // Rejects input name if it is < 5 or > 21 characters
            else if (newName.Length < 5 || newName.Length > 21)
                throw new ArgumentException("Error: Could not add user \"" + newName + "\" from file; usernames must contain between 5 and 21 characters");

            try
            {
                id = Convert.ToUInt32(userData[0]);
                userType = (UserType)Convert.ToUInt32(userData[1]);
                name = newName;
                passwordHash = userData[3];
                PostScore = Convert.ToInt32(userData[4]);
                CommentScore = Convert.ToInt32(userData[5]);
                ModeratingSubs = new string[0];
                for (int i = 6; i < userData.Length; i++)
                    ModeratingSubs.Append(userData[i]);
            }
            catch { throw new ArgumentException("Error: File input does not match format expected by [User] constructor"); }

            // Attempts to add user to global collection
            // Generates an ArgumentException if user is already in the collection
            try { Program.globalUsers.Add(Id, this); }
            catch (ArgumentException) { throw new ArgumentException("Error: Could not add user \"" + newName + "\"; the username was already taken"); }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        // Not needed for this iteration of the program (no sign up functionality included; only log in)
        // Alternate constructor (for creating a new user)
        // Enforces name format and uniqueness
        /*
        public User(string newName)
        {
            // Rejects input name if it begins or ends with space characters
            if (Char.IsWhiteSpace(newName, 0) || Char.IsWhiteSpace(newName, newName.Length - 1))
                throw new ArgumentException("Error: Usernames cannot begin or end with space characters. Please try again: ");

            // Rejects input name if it is < 5 or > 21 characters
            else if (newName.Length < 5 || newName.Length > 21)
                throw new ArgumentException("Error: Usernames must contain between 5 and 21 characters. Please try again: ");

            id = RedditUtilities.GenerateUniqueId();
            name = newName;
            PostScore = 0;
            CommentScore = 0;

            // Attempts to add user to global collection
            // Generates an ArgumentException if user is already in the collection
            try { Program.globalUsers.Add(Id, this); }
            catch (ArgumentException) { throw new ArgumentException("Error: That username is taken. Please try another: "); }
            catch (Exception e) { throw new Exception(e.Message); }
        }
        */

        // Defines User object comparison method
        // Required to implement IComparable interface
        // Sorts by name, in ascending order
        public int CompareTo(Object otherObject)
        {
            if (otherObject == null)
                throw new ArgumentNullException();

            // Typecasts object as a User
            User otherUser = otherObject as User;

            // Compares User objects by name if typecast was successful
            if (otherUser != null)
                return Name.CompareTo(otherUser.Name);
            else
                throw new ArgumentException("[User]:CompareTo argument is not a User.");
        }

        // Overrides ToString() method
        public override string ToString()
        {
            StringBuilder userDescription = new StringBuilder(Name);
            
            switch (Type)
            {
                case UserType.User:
                    for (int i = 0; i < 21 - Name.Length - 1; i++)
                        userDescription.Append(' ');
                    break;
                case UserType.Mod:
                    for (int i = 0; i < 16 - Name.Length - 1; i++)
                        userDescription.Append(' ');
                    userDescription.Append(" (M) ");
                    break;
                case UserType.Admin:
                    for (int i = 0; i < 16 - Name.Length - 1; i++)
                        userDescription.Append(' ');
                    userDescription.Append(" (A) ");
                    break;
            }

            userDescription.Append($"({PostScore} / {CommentScore})");

            return Convert.ToString(userDescription);
        }
        public enum UserType {
            User,
            Mod,
            Admin
        };
    }

    /****************************************************************************
    * This class contains information about the subreddits
    *
    * Atrributes:
    *   private readonly uint id;               unique identifier
        private string name;                    name of subreddit
        private uint members;                   total members in subreddit
        private uint active;                    active members in subreddit
        public SortedSet<uint> subPostIDs;      set of post under subreddit
    *
    * Properties:
    * Name          sets and returns the private name attribute
    * Members       returns the private members attribute
    * Active        returns the private active attribute
    * Id            returns the private id attribute
    *
    * Methods: none
    * 
    * Constructors: 
    * Subreddit()                   default
    * Subreddit(string[])           for input files
    * Subreddit(string)             for user-defined
    *
    * Restrictions:
    * a subreddit name may not have white space before or after it
    *   - it may also not be less than three characters or more than 21
    *
    * this class implements both the icomparable and Ienumerable interfaces
    *   warrants the use of foreach and sorting 
    ********************************************************************************/
    public class Subreddit : IComparable, IEnumerable
    {
        // attributes are private by default 
        private readonly uint id;
        private string name;
        private uint members;
        private uint active;
        public SortedSet<uint> subPostIDs;

        public uint Members { get { return members; } }
        public uint Active { get { return active; } }

        public string Name
        {
            get { return name; }
            set
            {
                // this if statements should match the restriction described in the assignment. Could it be any cleaner?
                if (value.Length <= 2 || value.Length > 21)
                    throw new ArgumentException("Error: Subreddit names must be between 3 and 21 characters");
                else if (value[0] == ' ' && value[value.Length - 1] == ' ')
                    throw new ArgumentException("Error: Subreddit names may not begin or end with white space");
                name = value;
            }
        }

        public uint Id => id;

        // this is the constructor that takes no arguments. 
        //what it really does is calling the constructor that takes 4 arguments(the one meant for input files) with 
        //values of 0 and "" (empty string)

        public Subreddit() : this(new string[] { "0", "", "0", "0" }) { } // this is the default constructor

        // constructor made for file input line tokens 
        public Subreddit(string[] parameters)
        {
            // this is assigning to the public property which already checks for valid names
            try { Name = parameters[1]; }
            catch (ArgumentException e) { throw new ArgumentException(e.Message); }

            try
            {
                id = Convert.ToUInt32(parameters[0]);
                members = Convert.ToUInt32(parameters[2]);
                active = Convert.ToUInt32(parameters[3]);
                subPostIDs = new SortedSet<uint>();
            }
            catch (ArgumentException) { throw new ArgumentException("Error: File input does not match format expected by [Subreddit] constructor."); }

            // Attempts to add subreddit to global collection
            // Generates an ArgumentException if subreddit is already in the collection
            try { Program.globalSubreddits.Add(Id, this); }
            catch (ArgumentException) { throw new ArgumentException("Error: Could not add subreddit \"" + Name + "\"; the subreddit name was already taken"); }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        // constructor made for user-created subreddits
        // THE USER DOES NOT HAVE THE OPTION OF CREATIN THE SUBREDDITS AS OF ASSIGNMENTS 1 & 2
/*        public Subreddit(string NameParam)
        {
            // this is assigning to the public property which already checks for valid names
            try { Name = NameParam; }
            catch (ArgumentException e) { throw new ArgumentException(e.Message); }

            id = RedditUtilities.GenerateUniqueId();
            members = 0;
            active = 0;
            subPostIDs = new SortedSet<uint>();

            // Attempts to add subreddit to global collection
            // Generates an ArgumentException if subreddit is already in the collection
            try { Program.globalSubreddits.Add(Id, this); }
            catch (ArgumentException) { throw new ArgumentException("Error: That subreddit already exists."); }
            catch (Exception e) { throw new Exception(e.Message); }
        }
*/
        public int CompareTo(Object otherObject)
        {
            if (otherObject == null)
                throw new ArgumentNullException();

            // Typecasts object as a Subreddit
            Subreddit otherSubreddit = otherObject as Subreddit;

            // Compares Subreddit objects if typecast was successful
            if (otherSubreddit != null)
                return Name.CompareTo(otherSubreddit.Name);
            else
                throw new ArgumentException("[Subreddit]:CompareTo argument is not a subreddit.");
        }

        // Overrides ToString() method
        public override string ToString()
        {
            return Name;
        }

        // Implements GetEnumerator method
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public SubEnum GetEnumerator()
        {
            return new SubEnum(subPostIDs.ToArray());
        }
    }

    /*
     * 
     * this class serves as an enumerator for the post class
     * 
     * Attributes:
     * subPostArray             a copy of the collection that is inside the subreddit 
     *                          class to enumerate through
     * position                 an integer used as index to denote the Current element
     *                           - when this is == -1, the enumerator is discarded
     * Properties:
     * Current                  getter that uses square brackets on the subPostArray
     *                          to return the current element denoted by position
     * Methods: 
     * MoveNext()               advances the current element by one through adding to
     *                          the position attribute and using it as index for the
     *                          subPostArray
     * 
     * this class implements the IEnumerator interface in order to allow the Subreddit
     *  class fully implement the IEnumerable
     * 
     ********************************************************************************/
    public class SubEnum : IEnumerator
    {
        public uint[] subPostArray;

        // Initializes enumerator to position before collection's first element
        int position = -1;

        public SubEnum(uint[] subPostIDs)
        {
            subPostArray = subPostIDs;
        }

        // private IEnumerator getEnumerator() { return (IEnumerator)this; }
        public bool MoveNext()
        {
            position++;
            return (position < subPostArray.Length);
        }
        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public uint Current
        {
            get
            {
                try
                {
                    return subPostArray[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

    }

    /* Class name: Comment
    * 
    * This class contains information about Reddit comments.
    * 
    * Attributes:
    * 
    * readonly uint id              Comment ID
    * readonly uint authorID        Author ID (User object)
    * string content                Comment text
    * readonly uint parentID        Parent ID (could be Post or Comment object)
    * uint upVotes                  Number of comment upvotes
    * uint downVotes                Number of comment downvotes
    * uint indentation              represents the level of the comment
    * 
    * readonly DateTime timeStamp                          Time comment was posted
    * SortedDictionary<uint, Comment> commentReplies       Collection of nested Comment objects
    * 
    * Properties:
    * 
    * Id                            Getter
    * AuthorID                      Getter
    * Content                       Getter & setter
    * ParentID                      Getter
    * UpVotes                       Getter & setter
    * DownVotes                     Getter & setter
    * TimeStamp                     Getter
    * Score                         Getter for UpVotes - DownVotes
    * Indentation                   Getter & setter
    * 
    * Constructors:
    * 
    * Comment()                     Default
    * Comment(string[])             For reading from file
    * Comment(string, uint, uint)   For user entered data
    * 
    * Restrictions:
    * 
    * User IDs must be unique
    * 
    * Other notes:
    * 
    * Sortable by Score using the IComparable interface
    * Enumerable through IEnumberable interface
    * Overrides ToString()
    */
    public class Comment : IComparable, IEnumerable
    {
        // Constant to help constructor for input files
        public static uint COMMENT_YEAR_INDEX = 6;

        // Attributes
        private readonly uint id;
        private uint authorID;
        private string content;
        private readonly uint parentID;
        private uint upVotes;
        private uint downVotes;
        private readonly DateTime timeStamp;
        public SortedDictionary<uint, Comment> commentReplies;
        private uint indentation;

        // Properties to control read/write access to private attributes
        public uint Id => id;
        public uint AuthorID
        {
            get { return authorID; }
            set { authorID = value; }
        }
        public string Content
        {
            get { return content; }
            set {
                if (value.Split().Intersect(RedditUtilities.badWords).Any())
                    throw new FoulLanguageException("Foul word was detected. Try posting again without foul language.\n");
                content = value; 
            
            }
        }
        public uint ParentID => parentID;
        public uint UpVotes
        {
            get { return upVotes; }
            set { upVotes = value; }
        }
        public uint DownVotes
        {
            get { return downVotes; }
            set { downVotes = value; }
        }
        public DateTime TimeStamp => timeStamp;
        public int Score => Convert.ToInt32(UpVotes) - Convert.ToInt32(DownVotes);
        public uint Indentation
        {
            get { return indentation; }
            set { indentation = value; }
        }
        public string AbbreviatedContent { get { return this.ToString("ListBox"); } }
        // Default constructor
        public Comment()
        {
            id = 0;
            authorID = 0;
            Content = "";
            parentID = 0;
            UpVotes = 0;
            DownVotes = 0;
            timeStamp = new DateTime(0);
            commentReplies = new SortedDictionary<uint, Comment>();
            Indentation = 0;
        }
        //      ID | AuthorID | Content | ParentID | UpVotes | DownVotes | Year | Month | Day | Hour | Min | Sec
        // OLD: ID | authorID | content | post/comment ID being replied to | upVotes | downVotes | year | month | day | hour | min | sec
        // Alternate constructor (for reading from a file)
        public Comment(string[] commentData, uint indentLevel)
        {
            try
            {
                id = Convert.ToUInt32(commentData[0]);
                authorID = Convert.ToUInt32(commentData[1]);
                Content = commentData[2];
                parentID = Convert.ToUInt32(commentData[3]);
                UpVotes = Convert.ToUInt32(commentData[4]);
                DownVotes = Convert.ToUInt32(commentData[5]);
                timeStamp = new DateTime(Convert.ToInt32(commentData[COMMENT_YEAR_INDEX + 0]),
                                         Convert.ToInt32(commentData[COMMENT_YEAR_INDEX + 1]),
                                         Convert.ToInt32(commentData[COMMENT_YEAR_INDEX + 2]),
                                         Convert.ToInt32(commentData[COMMENT_YEAR_INDEX + 3]),
                                         Convert.ToInt32(commentData[COMMENT_YEAR_INDEX + 4]),
                                         Convert.ToInt32(commentData[COMMENT_YEAR_INDEX + 5]));
                commentReplies = new SortedDictionary<uint, Comment>();
                Indentation = indentLevel;
            }
            catch { throw new Exception("Error: File input does not match format expected by [Comment] constructor"); }
        }

        // Alternate constructor (for creating a new comment)
        public Comment(string newContent, uint newAuthorID, uint newParentID, uint indentLevel)
        {
            id = RedditUtilities.GenerateUniqueId();
            authorID = newAuthorID;
            Content = newContent;
            parentID = newParentID;
            UpVotes = 1;
            DownVotes = 1;
            timeStamp = DateTime.Now;
            commentReplies = new SortedDictionary<uint, Comment>();
            Indentation = indentLevel;
        }

        // Defines Comment object comparison method
        // Required to implement IComparable interface
        // Sorts by score, in descending order
        public int CompareTo(Object otherObject)
        {
            if (otherObject == null)
                throw new ArgumentNullException();

            // Typecasts object as a Comment
            Comment otherComment = otherObject as Comment;

            // Compares Comment objects by score if typecast was successful
            if (otherComment != null)
            {
                // Sort in descending order
                if (Score > otherComment.Score)
                    return -1;
                else if (Score == otherComment.Score)
                    return 0;
                else
                    return 1;
            }
            else
                throw new ArgumentException("[Comment]:CompareTo argument is not a comment.");
        }

        // Overrides ToString() method (for showing full comment content)
        // Does not include indentation
        public override string ToString()
        {
            return $"<{Id}> ({Score}) {Content} - {Program.globalUsers[AuthorID].Name} |{TimeStamp:G}|\n";
        }

        // Overloads ToString() method (for showing abbreviated comment content)
        // Includes indentation
        public string ToString(string shortTitle)
        {
            string commentDescription = "";

            // Adds indentation
            for (int i = 0; i < Indentation; ++i)
                commentDescription += RedditUtilities.INDENTATION;

            // Shortens title if needed
            StringBuilder newTitle = new StringBuilder(Content);
            if (shortTitle == "ListBox" && Content.Length > 35)
            {
                newTitle.Remove(35, Content.Length - 35);
                newTitle.Append("...");
            }

            commentDescription += $"<{Id}> ({Score}) {newTitle.ToString()} - { Program.globalUsers[AuthorID].Name} |{TimeStamp:G}|";
            return commentDescription;
        }

        // Implements GetEnumerator method
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public CommentEnum GetEnumerator()
        {
            return new CommentEnum(commentReplies.Values.ToArray());
        }
    }
    /*
    * 
    * this class serves as an enumerator for the Comment class
    * 
    * Attributes:
    * commentReplyArray             a copy of the collection that is inside the Comment 
    *                               class to enumerate through
    * position                 an integer used as index to denote the Current element
    *                           - when this is == -1, the enumerator is discarded
    * Properties:
    * Current                  getter that uses square brackets on the commentReplyArray
    *                          to return the current element denoted by position
    * Methods: 
    * MoveNext()               advances the current element by one through adding to
    *                          the position attribute and using it as index for the
    *                          commentReplyArray
    * 
    * this class implements the IEnumerator interface in order to allow the Comment 
    *  class to fully implement the IEnumerable
    * 
    ********************************************************************************/
    public class CommentEnum : IEnumerator
    {
        public Comment[] commentReplyArray;

        // Initializes enumerator to position before collection's first element
        int position = -1;

        public CommentEnum(Comment[] commentReplies)
        {
            commentReplyArray = commentReplies;
        }

        public bool MoveNext()
        {
            position++;
            return (position < commentReplyArray.Length);
        }
        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public object Current
        {
            get
            {
                try
                {
                    return commentReplyArray[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
    /****************************************************************************
    * This class contains information about the Posts objects
    *
    * Atrributes:
    * private bool locked                                         boolean to make instance uncomment-able
    * private readonly uint id;                                   unique identifier
      private string title;                                       title of the post
      private readonly uint authorID;                             author of the post(equals the id of a user object)
      private string postContent;                                 content of the post
      private readonly uint subHome; // also called subreddit Id  the id of the subreddit this post belongs to
      private uint upVotes;                                       upvotes of the post
      private uint downVotes;                                     downvotes of the post
      private uint weight;                                        represents the importance of a post using score
      private readonly DateTime timeStamp;                        the time a post was made
      public SortedDictionary<uint, Comment> postComments;        collection of comments under this post
    *
    * Properties:
    * Score         returns a calculation based off upvotes and downvotes
    * Locked        locked boolean modifier
    * PostRating    Score modifier that's based off the weight of the score
    * Id            returns the corresponding id property of the post
    * SubHomeId     same as above
    * Title         same as above
    * AuthorID      same as above
    * DateString    returns a string representation of the post's creation date
    *
    * Methods: none
    * 
    * Constructors: 
    * Post()                   default
    * Post(string[])           for input files
    * Post(string UserTitle, uint AuthorId, string UserPostContent, uint subredditId) for user-defined posts
    * 
    *
    * this class implements both the icomparable and Ienumerable interfaces
    *   warrants the use of foreach and sorting 
    ********************************************************************************/
    public class Post : IComparable, IEnumerable
    {
        // private properties
        private bool locked;
        private readonly uint id;
        private string title;
        private uint authorID;
        private string postContent;
        private readonly uint subHome; // also called subreddit Id
        private uint upVotes;
        private uint downVotes;
        private uint weight;
        private readonly DateTime timeStamp;
        public SortedDictionary<uint, Comment> postComments;

        // public setters and getters for post content and title
        public int Score => (int)upVotes - (int)downVotes;

        public bool Locked
        {
            get { return locked; }
            set { locked = value; }
        }
        public float PostRating
        {
            get
            {
                switch (weight)
                {
                    case 0: return Score;
                    case 1: return Score * 0.66f;
                    default: return 0;
                }
            }
        }
        public string PostContent
        {
            get { return postContent; }
            // this assumes that return characters are not allowed when creating content for reddit
            set
            {
                string[] tokenizedContentString = value.Split(' ');
                if (RedditUtilities.badWords.Intersect(tokenizedContentString).Any())
                    throw new FoulLanguageException("Foul word was detected. Try posting again without foul language.\n");
                if (value.Length > 1 && value.Length < 1000)
                    postContent = value;
                else
                    throw new ArgumentException("Bad Content length provided. Be sure that your Content is more than one character and lesss than a thousand.\n");

            }
        }
        public string Title
        {
            get { return title; }
            set
            {
                string[] tokenizedTitle = value.Split(' ');
                if (RedditUtilities.badWords.Intersect(tokenizedTitle).Any())
                    throw new FoulLanguageException("Foul word was detected. Entitle again without foul language.\n");
                if (value.Length > 1 && value.Length < 100)
                    title = value;
                else
                    throw new ArgumentException("Bad Title length provided. Be sure that your Title is more than one character and less than a hundred.\n");
            }
        }
        public uint Id => id;
        public uint subHomeId { get { return subHome; } }
        public string DateString { get { return timeStamp.ToString("G"); } }
        public uint AuthorId
        {
            get { return authorID; }
            set { authorID = value; }
        }
        public string AbbreviateContent { get { return this.ToString("ListBox"); } }

        public Post()
        {
            Locked = false;
            id = 0;
            title = "";
            authorID = 0;
            postContent = "";
            subHome = 0; // also called subreddit Id
            upVotes = 0;
            downVotes = 0;
            weight = 0;
            timeStamp = new DateTime();
            postComments = new SortedDictionary<uint, Comment>();
        }

        // OLD: unique ID | authorID | title | content | subredditID | upVotes | downVotes | weight | year | month | day | hour | min | sec
        // Locked | ID | AuthorID | Title | Content | SubredditID | UpVotes | DownVotes | Weight | Year | Month | Day | Hour | Min | Sec
        // this constructor assumes the order of data and assumes unique Id's
        public Post(string[] parameters)
        {
            try
            {
                Title = parameters[3];
                PostContent = parameters[4];
            }
            catch (FoulLanguageException)
            {
                // Creates post anyway due to sample output example
                throw new FoulLanguageException("Warning: Title or content for post " + parameters[1] + " does not meet parameters; adding anyway");
            }
            catch (ArgumentException)
            {
                // Creates post anyway due to sample output example
                throw new ArgumentException("Warning: Title or content for post " + parameters[1] + " does not meet parameters; adding anyway");
            }
            finally
            {
                try
                {
                    switch(parameters[0])
                    {
                        case "0":
                            Locked = false;
                            break;
                        case "1":
                            Locked = true;
                            break;
                    }
                    id = Convert.ToUInt32(parameters[1]);
                    authorID = Convert.ToUInt32(parameters[2]);

                    title = parameters[3];
                    postContent = parameters[4];

                    subHome = Convert.ToUInt32(parameters[5]);
                    upVotes = Convert.ToUInt32(parameters[6]);
                    downVotes = Convert.ToUInt32(parameters[7]);
                    weight = Convert.ToUInt32(parameters[8]);

                    // safe guard in case the default constructor is used (it is unknown if the default constructor will ever get used)
                    if (parameters.Length > 8)
                        timeStamp = new DateTime(Convert.ToInt32(parameters[9]),
                                                    Convert.ToInt32(parameters[10]),
                                                    Convert.ToInt32(parameters[11]),
                                                    Convert.ToInt32(parameters[12]),
                                                    Convert.ToInt32(parameters[13]),
                                                    Convert.ToInt32(parameters[14]));
                    postComments = new SortedDictionary<uint, Comment>();
                }
                catch { throw new Exception("Error: Could not add post" + id + "; file input does not match format expected by [Post] constructor"); }

                // Attempts to add post to global collection
                // Generates an ArgumentException if post is already in the collection
                try { Program.globalPosts.Add(Id, this); }
                catch (ArgumentException) { throw new ArgumentException("Error: Could not add post" + id + "; the post ID was already taken"); }
                catch (Exception e) { throw new Exception(e.Message); }

                // Attempts to add post ID to subreddit collection
                // Generates an ArgumentException if post ID is already in the collection
                try { Program.globalSubreddits[subHomeId].subPostIDs.Add(Id); }
                catch (KeyNotFoundException) { throw new KeyNotFoundException("Error: Could not add post " + Id + "; the subreddit was not found"); }
                catch (ArgumentException) { throw new ArgumentException("Error: Could not add post " + Id + "; the post ID was already taken"); }
                catch (Exception e) { throw new Exception(e.Message); }
            }
         }

        // constructor for user generated Post instances
        // new posts have the current time assigned to them
        public Post(bool LockedBool, string UserTitle, uint AuthorId, string UserPostContent, uint subredditId)
        {
            Locked = LockedBool;
            id = RedditUtilities.GenerateUniqueId();
            upVotes = 1;
            downVotes = 0;
            weight = 0;
            try
            {
                Title = UserTitle;
                PostContent = UserPostContent;
            }
            catch (FoulLanguageException e) { throw new FoulLanguageException(e.Message); }
            catch (ArgumentException e) { throw new ArgumentException(e.Message); }
            catch (Exception e) { throw new Exception(e.Message); }
            authorID = AuthorId;
            subHome = subredditId;
            postComments = new SortedDictionary<uint, Comment>();

            timeStamp = DateTime.Now;

            // Attempts to add post to global collection
            // Generates an ArgumentException if post is already in the collection
            try { Program.globalPosts.Add(Id, this); }
            catch (ArgumentException) { throw new ArgumentException("Error: That post already exists."); }
            catch (Exception e) { throw new Exception(e.Message); }

            // Attempts to add post ID to subreddit collection
            // Generates an ArgumentException if post ID is already in the collection
            try { Program.globalSubreddits[subHomeId].subPostIDs.Add(Id); }
            catch (KeyNotFoundException) { throw new KeyNotFoundException("Error: Could not add post to that subreddit."); }
            catch (ArgumentException) { throw new ArgumentException("Error: Could not add post to that subreddit."); }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public int CompareTo(Object otherObject)
        {
            if (otherObject == null)
                throw new ArgumentNullException();

            // Typecasts object as a Post
            Post otherPost = otherObject as Post;

            // Compares Post objects by score if typecast was successful
            if (otherPost != null)
            {
                // Sort in descending order by PostRating
                if (PostRating > otherPost.PostRating)
                    return -1;
                else if (PostRating == otherPost.PostRating)
                    return 0;
                else
                    return 1;
            }
            else
                throw new ArgumentException("[Post]:CompareTo argument is not a post.");
        }
        
        // Overrides ToString() method (for showing full post content)
        public override string ToString()
        {
            return $"<{Id}> [{Program.globalSubreddits[subHomeId].Name}] ({Score}) {Title} {postContent} - {Program.globalUsers[AuthorId].Name} |{DateString}|\n";
        }
        
        // Overloads ToString() method (for showing abbreviated post content)
        public string ToString(string shortTitle)
        {
            StringBuilder newTitle = new StringBuilder(Title);

            if (shortTitle == "ListBox" && Title.Length > 35)
            {
                newTitle.Remove(35, Title.Length - 35);
                newTitle.Append("...");
            }

            string lockStatus = Locked == true ? " **LOCKED** " : " ";
            return $"<{Id}> [{Program.globalSubreddits[subHomeId].Name}] ({ Score}) {newTitle.ToString()}{lockStatus}- { Program.globalUsers[AuthorId].Name} |{ DateString}|";
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public PostEnum GetEnumerator()
        {
            return new PostEnum(postComments.Values.ToArray());
        }
    }
    /*
     * This class is a derived from the Exception class
     * Attributes: NONE
     * Properties: NONE
     * Methoids: NONE
     * Constructors:
     * FoulLanguageException()                                  empty constructor
     * FoulLanguageException(string message)                    calls the base exception constructor which accepts one string parameter
     * FoulLanguageException(string message, Exception inner)   same as above
     * 
     ************************************************************************************************************************************/
    public class FoulLanguageException : Exception
    {
        public FoulLanguageException() { }
        public FoulLanguageException(string message) : base(message) { }
        public FoulLanguageException(string message, Exception inner) : base(message, inner) { }
    }
    /*
    * 
    * this class serves as an enumerator for the Post class
    * 
    * Attributes:
    * commentArray             a copy of the collection that is inside the Post 
    *                           class to enumerate through
    * position                 an integer used as index to denote the Current element
    *                           - when this is == -1, the enumerator is discarded
    * Properties:
    * Current                  getter that uses square brackets on the commentArray
    *                          to return the current element denoted by position
    * Methods: 
    * MoveNext()               advances the current element by one through adding to
    *                          the position attribute and using it as index for the
    *                          commentArray
    * 
    * this class implements the IEnumerator interface in order to allow the Comment 
    *  class to fully implement the IEnumerable
    * 
    ********************************************************************************/
    public class PostEnum : IEnumerator
    {
        int position;
        Comment[] commentArray;
        public PostEnum(Comment[] _commentArray)
        {
            position = -1;
            commentArray = _commentArray;
        }

        bool IEnumerator.MoveNext()
        {
            ++position;
            return (position < commentArray.Length);
        }

        void IEnumerator.Reset()
        {
            position = -1;
        }

        public object Current
        {
            get
            {
                try
                {
                    return commentArray[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }

    /* Class name: Utilities
     * 
     * Main functionality implemented:
     *     
     * Atributes:
     * Indentation constant
     * Quit constants in array
     * Foul language in array
     * File reader
     * 
     * Methods:
     * Random ID generator
     *      Ensures IDs are unique
     *      ID initializers in other classes call GenerateUniqueId(), which calls testUnique()
     * 
     * Indentation generator
     *      Code in form calls FinalIndentation(uint level)
     *      
     * Comment parent finder
     *      Code in other classes call FindCommentParent, which calls CommentTreeTraverseFunction()
     *      
     * CommentTreeTraverseFunction
     *      visits a comment tree to return a parent comment nested in another comment. this is an extension
     *      to the comment parent finder described above
     *      
     * String token finder
     *      Code in other classes call FindToken()
     *      
     * ReadFiles
     *      called upon first thing in the windows form associated with this C# file
     *      reads the contents of post, users, comments, and subreddit files
     * 
     */
    public static class RedditUtilities
    {
        // Indentation constant
        // Defines constant to be used together with comment indentation level

        public static string INDENTATION = "      ";



        // Quit array
        // Values accepted from user to quit program

        public static string[] quitArray = new string[] { "q", "e", "quit", "exit" };

        // Foul language collection
        public static readonly string[] badWords;

        // Random ID generator
        // Ensures IDs are unique; considers users, subreddits, posts, and comments

        private static readonly Random random;

        // Initializes random number object
        // Initializes foul language list for filter
        static RedditUtilities()
        {
            random = new Random();
            badWords = new string[] { "fudge", "shoot", "baddie", "butthead", };
        }

        /* Method name: testUnique
         * 
         * Tests random number for uniqueness (used for IDs)
         * 
         * Parameters:
         * uint randomNumber     Generated by GenerateUniqueId()
         * 
         * Returns:
         * bool
         * 
         * Notes:
         * This function is recursive
         */
        private static bool testUnique(uint randomNumber)
        {
            bool boolResult = true;
            if (Program.globalUsers.ContainsKey(randomNumber)) return false;
            if (Program.globalSubreddits.ContainsKey(randomNumber)) return false;
            if (Program.globalPosts.ContainsKey(randomNumber)) return false;

            Action<Comment> treeTraverseFunction = null;
            treeTraverseFunction = (Comment comment) => {
                // NOTE: the foreach here assumes that the comment class in enumerable in a way that goes over every child comment in a comment
                if (comment.Id != randomNumber) foreach (Comment childComment in comment)
                    {
                        if (boolResult == false) break;
                        treeTraverseFunction(childComment);
                    }
                else boolResult = false;
            };

            foreach (Post currentPost in Program.globalPosts.Values)
            {

                if (boolResult == false) break;
                else
                {
                    foreach (KeyValuePair<uint, Comment> currentComment in currentPost.postComments)
                    {
                        if (boolResult == false) break;
                        treeTraverseFunction(currentComment.Value);
                    }
                }
            }

            return boolResult;
        }

        /* Method name: FinalIndentation
         * 
         * Returns last visible level of comments; determined in form PrintChildComments method
         * 
         * Parameters:
         * uint indentationLevel    last visible level of comments (with count starting from 1)
         * 
         * Returns:
         * string                   indentation needed for last visible level of comments
         */
        public static string FinalIndentation(uint indentationLevel)
        {
            string indent = "";
            for (uint i = 0; i < indentationLevel; i++)
                indent += RedditUtilities.INDENTATION;
            return indent;
        }

        /* Method name: CommentReplyAdder
        * 
        * returns a comment object that holds the given id. this function is 
        * almost identical to adding to test Unique as it needs to traverse each
        * Comment (node) and its children in attempt to finding the Comment (node)
        * with the matching value
        * 
        * Parameters
        * uint parentId                  Given parent ID
        *  
        * Returns:
        * Comment               If found: parent comment, else it returns null
        * 
        * Notes:
        * Throws Exception if parent comment could not be found
        */
        static public Comment CommentReplyAdderExtension(uint parentId)
        {
            Comment foundParent = null;
            Action<Comment> treeTraverseFunction = null;
            treeTraverseFunction = (Comment potentialParentComment) => {
                // NOTE: the foreach here assumes that the comment class in enumerable in a way that goes over every child comment in a comment
                if (potentialParentComment.Id != parentId) foreach (Comment childComment in potentialParentComment)
                    {
                        if (foundParent != null) break;
                        treeTraverseFunction(childComment);
                    }
                else foundParent = potentialParentComment;
            };

            foreach (Post currentPost in Program.globalPosts.Values)
            {

                if (foundParent != null) break;
                else
                {
                    foreach (KeyValuePair<uint, Comment> currentComment in currentPost.postComments)
                    {
                        if (foundParent != null) break;
                        treeTraverseFunction(currentComment.Value);
                    }
                }
            }
            return foundParent;
        }

        /* Method name: GenerateUniqueId
         * 
         * Generates unique random number
         * 
         * Returns:
         * uint                  Random number generated
         * 
         * Notes:
         * Calls recursive function testUnique
         * Called from User, Subreddit, Post, and Comment classes
         */
        public static uint GenerateUniqueId()
        {
            uint uniqueId;
            do uniqueId = Convert.ToUInt32(random.Next(0, 10000)); while (!testUnique(uniqueId));
            return uniqueId;
        }

        /* Method name: CommentTreeTraverseFunction
         * 
         * Traverses comment tree looking for the parent of a particular comment
         * 
         * Parameters:
         * Comment               Given comment object
         * uint                  ID of the parent
         *  
         * Returns:
         * Comment               If found: parent comment
         * null                  If not found: null
         * 
         * Notes:
         * Recursive function called from FindCommmentParent
         */
        public static Comment CommentTreeTraverseFunction(Comment currentComment, uint potentialParentID)
        {
            foreach (Comment childComment in currentComment.commentReplies.Values)
            {
                if (currentComment.Id == potentialParentID)
                    return currentComment;
                else
                    return CommentTreeTraverseFunction(childComment, potentialParentID);
            }

            return null;
        }

        /* Method name: FindCommentParent
         * 
         * Serves as the initial call to the recursive function CommentTreeTraverseFunction
         * 
         * This method needed to be isolated as each Comment under a post represents an entire 
         * tree, and those trees must be operated on one by one
         * 
         * Parameters:
         * uint potentialParentID          ID of the parent
         *  
         * Returns:
         * Comment               If found: parent comment
         * 
         * Note:                 If not found: exception thrown
         */
        public static Comment FindCommentParent(uint potentialParentID)
        {
            Comment returnedComment = null;

            // Start from comments attached to posts
            foreach (Post currentPost in Program.globalPosts.Values)
            {
                foreach (Comment currentComment in currentPost.postComments.Values)
                {
                    if (currentComment.Id == potentialParentID)
                        return currentComment;

                    // Else
                    returnedComment = CommentTreeTraverseFunction(currentComment, potentialParentID);
                }
            }

            if (returnedComment == null)
                throw new Exception("Error: A parent comment could not be found");
            else
                return returnedComment;
        }

        /* Method name: ReadFiles
         * 
         * Initializes "existing" users, subreddits, posts, and comments with data from files
         * 
         * Parameters:
         * N/A
         *  
         * Returns:
         * string[]              Log of exceptions caught in this method
         */
        public static List<string> ReadFiles()
        {
            // Initializes "existing" users, subreddits, posts, and comments with data from files
            string filePrefix = "..\\..\\";
            string[] fileNames = new string[] { "users", "subreddits", "posts", "comments" };
            string fileLine;

            // Declares log for exceptions caught in this method (to be passed back to the form)
            List<string> fileErrors = new List<string>();

            // Processes each file
            foreach (string fileName in fileNames)
            {
                if (File.Exists(filePrefix + fileName + ".txt"))
                {
                    // Opens file
                    using (StreamReader inFile = new StreamReader(filePrefix + fileName + ".txt"))
                    {
                        // Reads first line of file
                        fileLine = inFile.ReadLine();

                        // Constructs relevant object and adds it to relevant collection
                        while (fileLine != null)
                        {
                            switch (fileName)
                            {
                                case "users":
                                    try { User newUser = new User(fileLine.Split('\t')); }
                                    catch (ArgumentException e) { fileErrors.Add(e.Message); }
                                    catch (Exception e) { fileErrors.Add(e.Message); }
                                    break;
                                case "subreddits":
                                    try { Subreddit newSubreddit = new Subreddit(fileLine.Split('\t')); }
                                    catch (ArgumentException e) { fileErrors.Add(e.Message); }
                                    catch (Exception e) { fileErrors.Add(e.Message); }
                                    break;
                                case "posts":
                                    try { Post newPost = new Post(fileLine.Split('\t')); }
                                    catch (KeyNotFoundException e) { fileErrors.Add(e.Message); }
                                    catch (FoulLanguageException e) { fileErrors.Add(e.Message); } // Warns, but doesn't scold user, for foul language
                                    catch (ArgumentException e) { fileErrors.Add(e.Message); }     // Warns, but doesn't scold user, for title / post length
                                    catch (Exception e) { fileErrors.Add(e.Message); }
                                    break;
                                case "comments":
                                    try
                                    {
                                        string[] tokenArray = fileLine.Split('\t');
                                        uint commentParentId= Convert.ToUInt32(tokenArray[3]);

                                        // Adds comment to parent post, if applicable
                                        if (Program.globalPosts.ContainsKey(commentParentId))
                                        {
                                            Comment newComment = new Comment(tokenArray,0);
                                            Program.globalPosts[commentParentId].postComments.Add(newComment.Id, newComment);
                                        }

                                        // Adds comment to parent comment, if applicable
                                        else
                                        {
                                            Comment parent = FindCommentParent(commentParentId);
                                            Comment newComment = new Comment(tokenArray, parent.Indentation+1);
                                            parent.commentReplies.Add(newComment.Id, newComment);
                                        }
                                    }
                                    catch (ArgumentException e) { fileErrors.Add(e.Message); }
                                    catch (Exception e) { fileErrors.Add(e.Message); }
                                    break;
                                default:
                                    fileErrors.Add("Error: Could not process " + fileName + ".txt");
                                    break;
                            }

                            // Reads next line of file
                            fileLine = inFile.ReadLine();
                        }
                    }
                }
                else
                    fileErrors.Add("Error: Could not open input file " + fileName + ".txt");
            }
            return fileErrors;
        }
    }
    /***************************************************************************************************************
     * Class name: Program
     * this class contains Main, representing the entrypoint of the program
     * 
     * Attributes:
     * SortedDictionary<uint, User> globalUsers             global collection of all the users in the program
     * SortedDictionary<uint, Subreddit> globalSubreddits   global collection of all the subreddits in the program
     * SortedDictionary<uint, Post> globalPosts             global collection of all the posts in the program
     * 
     * User activeUser                                      retains the initial value of 'null'. This variable is 
     *                                                       - assigned to during runtime when the program user 
     *                                                       - successfully signs into the program.
     * Other notes:
     *  the Main program contains generic statements for running a windows forms application.
     * 
     ***************************************************************************************************************/
    static class Program
    {
        // Defines "global" collections
        public static SortedDictionary<uint, User> globalUsers = new SortedDictionary<uint, User>();
        public static SortedDictionary<uint, Subreddit> globalSubreddits = new SortedDictionary<uint, Subreddit>();
        public static SortedDictionary<uint, Post> globalPosts = new SortedDictionary<uint, Post>();

        // Declares active (logged-in) user variable
        public static User activeUser = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
