export type CurrentUser = {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  displayName: string;
  legacyId?: number | null;
  avatarUrl?: string | null;
  bio?: string | null;
  signature?: string | null;
  timeZoneId?: string | null;
  roles: string[];
};
