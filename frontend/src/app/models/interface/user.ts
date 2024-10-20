export interface AppUser {
     phoneNumber: string;
     createdAt: string;
     id: string;
     userName: string;
     normalizedUserName: string;
     email: string;
     normalizedEmail: string;
     profilePictureUrl: string;
     preferredLanguage: string;
     preferredCurrency: string;
     // emailConfirmed: boolean;
     // passwordHash: string;
     // securityStamp: string;
     // concurrencyStamp: string;
     // phoneNumberConfirmed: boolean;
     // twoFactorEnabled: boolean;
     // lockoutEnd: string | null;
     // lockoutEnabled: boolean;
     // accessFailedCount: number;
}
