# File Uploader Api
.NET test application

## User flow

1. Register. 
   Use endpoint `api/auth/register` to register. For example, you may use folowing data:
   {
       "email": "test@test.com",
       "password": "123",
       "name": "test",
       "lastName": "test"
   }

2. Login.
   Use endpoint `api/auth/login` to login. Enter the email and password you used for registration.
   After that, there will be a token in the response. Copy it and save it. It will be used for further requests

# Accessible only for authenticated users.
3. Upload file(s).
   Use endpoint `api/file-group/upload` to upload file(s).

4. Get the progress of uploading file(s)
   Use endpoint `api/file-group/progress[?fileName=]` to get uploading progress.
   If you are uploading a group of files, you can specify the name of a particular file to see the progress of its upload.

5. List of uploaded files.
   Use endpoint `api/file-group/uploaded` to view a list of uploaded files or groups of files.

6. Download uploaded file(s)
   Use endpoint `api/file-group/download/{uploaded file group id. You can get it from list}` to download own files.

7. Generate one-time downloading link.
   Use endpoint `api/file-group/generate/{uploaded file group id. You can get it from list}/link`
   to generate one-time downloading link.

# Accessible for all users.

8. Download file(s) by link.
