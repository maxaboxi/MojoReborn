export interface PageMenuItem {
  id: number;
  parentId: number | null;
  title: string;
  url: string;
  featureName?: string | null;
  viewRoles: string;
  order: number;
  children: PageMenuItem[];
}
