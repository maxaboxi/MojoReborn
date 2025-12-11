export type CurrentUser = {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  displayName: string;
  avatarUrl?: string | null;
  bio?: string | null;
  signature?: string | null;
  timeZoneId?: string | null;
  roles: string[];
};
