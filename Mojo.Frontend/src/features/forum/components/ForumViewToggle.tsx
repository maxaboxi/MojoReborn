import { ToggleButton, ToggleButtonGroup } from '@mui/material';
import ViewAgendaIcon from '@mui/icons-material/ViewAgenda';
import AccountTreeIcon from '@mui/icons-material/AccountTree';
import type { ForumViewMode } from '../types/forum.types';

interface ForumViewToggleProps {
  value: ForumViewMode;
  onChange: (mode: ForumViewMode) => void;
}

export const ForumViewToggle = ({ value, onChange }: ForumViewToggleProps) => (
  <ToggleButtonGroup
    value={value}
    exclusive
    onChange={(_event, next) => {
      if (next) {
        onChange(next);
      }
    }}
    size="small"
    color="primary"
  >
    <ToggleButton value="classic" aria-label="Classic thread view">
      <ViewAgendaIcon fontSize="small" />
      Classic
    </ToggleButton>
    <ToggleButton value="nested" aria-label="Reddit-style nested view">
      <AccountTreeIcon fontSize="small" />
      Nested
    </ToggleButton>
  </ToggleButtonGroup>
);
