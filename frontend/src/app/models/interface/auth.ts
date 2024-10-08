export interface RegisterDto {
     userName: string;
     email: string;
     password: string;
     phoneNumber: string;
}
export interface LoginDto {
     userNameOrEmail: string;
     password: string;
}

export interface UpdateUserDto {
     userName?: string;
     email?: string;
     phoneNumber?: string;
     oldPassWord?: string;
     newPassWord?: string;
}