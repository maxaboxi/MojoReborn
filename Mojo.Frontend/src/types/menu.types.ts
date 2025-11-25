export interface PageMenuItem {
  id: number;
  parentId: number | null;
  title: string;
  url: string;
  viewRoles: string;
  order: number;
  children: PageMenuItem[];
}
